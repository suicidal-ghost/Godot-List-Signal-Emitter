using Godot;
using System.Collections.Generic;
using System;

public class ParetnNode : Spatial
{
    float DeltaTime;
    Spatial ChildNode;
    List<uint> uintList = new List<uint>();
    int TestTime = 10;

    [Signal]
    public delegate void RequestList();

    public override void _Ready()
    {
        DeltaTime = 0;
    }

    public override void _Process(float delta) {
        DeltaTime += delta;
        CheckTime();
        uintList = new List<uint>();
        EmitSignal(nameof(RequestList));
    }

    public void _on_Child_Register (Spatial s) {
        ChildNode = s;
        this.Connect(nameof(RequestList), ChildNode, "_on_List_Request");
    }

    public void CheckTime() {
        if (DeltaTime >= (float)TestTime) {
            //GetChild(0).QueueFree();
            GD.Print("Elapsed Time: " + DeltaTime);
            GD.Print("List Count: " + uintList.Count);
            //GetTree().Quit();
        }
    }

    public void _on_ListElement_Emit(uint u) {
        
        uintList.Add(u);
    }

}
