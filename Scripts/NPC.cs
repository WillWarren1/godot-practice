using Godot;
using System;

public class NPC : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Console.WriteLine("waiting");
        GetNode("../player").Connect("_BloodSeeker", this, nameof(_OnReceiveBloodSeeker));

    }

    public void _OnReceiveBloodSeeker(string value)
    {
        Console.WriteLine("Received Signal, suck my blood pls " + value);
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
