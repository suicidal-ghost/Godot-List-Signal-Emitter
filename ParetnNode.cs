using Godot;
using System.Collections.Generic;
using System;

public class ParetnNode : Spatial
{
    float DeltaTime;
    Spatial ChildNode;
    List<uint> uintList = new List<uint>();
    int TestTime = 10;

    //emit to child node to request the list be sent one element at a time
    [Signal]
    public delegate void RequestList();

    public override void _Ready()
    {
        DeltaTime = 0;
    }

    public override void _Process(float delta) {
        DeltaTime += delta;
        CheckTime();
        uintList = new List<uint>();       //reinitialize list before requesting it
        EmitSignal(nameof(RequestList));   //comment this out when control testing
    }

    //cannot connect to child node until it is ready
    public void _on_Child_Register (Spatial s) {
        ChildNode = s;
        this.Connect(nameof(RequestList), ChildNode, "_on_List_Request");
    }

    //can uncomment the two lines below to have the child node print the list count and close the application when DeltaTime > TestTime. However, this will cause Godot to crash after execution.
    public void CheckTime() {
        if (DeltaTime >= (float)TestTime) {
            //GetChild(0).QueueFree();
            GD.Print("Elapsed Time: " + DeltaTime);
            GD.Print("List Count: " + uintList.Count);
            //GetTree().Quit();
        }
    }

    //recieves list element and adds it the list on this node
    public void _on_ListElement_Emit(uint u) {       
        uintList.Add(u);
    }

}
