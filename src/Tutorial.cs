using Godot;
using System;

public class Tutorial : CanvasLayer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        GetNode<Timer>("Timer").Connect("timeout", this, nameof(_on_timer_timeout));
    }

    public void _on_timer_timeout() {
        GetNode<Label>("CenterContainer/PanelContainer/HBoxContainer/Label").Text =
            "In this game, you have the power to control the\nwind. By skillfully directing the wind’s course, you\ncan navigate ships through treacherous waters,\navoid obstacles, and bring them safely to their\ndestination.\n\n";
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
