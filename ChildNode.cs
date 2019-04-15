using Godot;
using System.Collections.Generic;
using System;

public class ChildNode : Spatial
{
    //emits signal when this node is ready so that parent node can connect to it
    [Signal]
    public delegate void ChildReady(Spatial s);

    //for sending each list element 
    [Signal]
    public delegate void SendListElement(uint u);

    Random random;
    List<uint> childList = new List<uint>();

    public override void _Ready()
    {
        Spatial ParentNode = GetParent<Spatial>();
        this.Connect(nameof(ChildReady), ParentNode, "_on_Child_Register");
        EmitSignal(nameof(ChildReady), this);
        random = new Random();
        this.Connect(nameof(SendListElement), ParentNode, "_on_ListElement_Emit");
    }

    // can uncomment this and comment out the foreach statement in _on_List_Request() for a control test that will only generate and add an element to the list every frame without emmitting it.
    // public override void _Process(float delta) {
    //     _on_List_Request();   
    // }

    //Generates a maximum of a 32 bit uint (can be less depending on RNG) end emits all the list elements to the parent node
    public void _on_List_Request() {
        uint thirtybits = (uint)random.Next(1 << 30);
        uint twobits = (uint)random.Next(1 << 2);
        uint finaluint = (thirtybits << 2) | twobits;
        childList.Add(finaluint);
        foreach(uint u in childList) {
            EmitSignal(nameof(SendListElement), u);
        }
    }

    //this can be used to get childlist count when the timer runs out buy uncommenting the GetChild(0).QueueFree(); line in CheckTime() on the Parent Node. This will cause this program to crash however. 
    private void _on_ChildNode_tree_exiting() {
       GD.Print("ChildListCount: " + childList.Count);
    }



}




