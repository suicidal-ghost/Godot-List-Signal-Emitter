using Godot;
using System.Collections.Generic;
using System;

public class ChildNode : Spatial
{
    [Signal]
    public delegate void ChildReady(Spatial s);
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

    // Called every frame. 'delta' is the elapsed time since the previous frame.
   // public override void _Process(float delta) {
        
   // }

    public void _on_List_Request() {
        uint thirtybits = (uint)random.Next(1 << 30);
        uint twobits = (uint)random.Next(1 << 2);
        uint finaluint = (thirtybits << 2) | twobits;
        childList.Add(finaluint);
        foreach(uint u in childList) {
            EmitSignal(nameof(SendListElement), u);
        }
    }

    private void _on_ChildNode_tree_exiting() {
       GD.Print("ChildListCount: " + childList.Count);
    }



}




