using Godot;
using System;

public class PauseMenu : Control {

    private Button unpause;
    private Button levels;
    private Button exit;
    
    [Export] private PackedScene levelScreen;

    public override void _Ready() {
        unpause = GetNode<Button>("CenterContainer/VBoxContainer/continue");
        levels = GetNode<Button>("CenterContainer/VBoxContainer/levels");
        exit = GetNode<Button>("CenterContainer/VBoxContainer/exit");
        
        levels.GrabFocus();
        
        unpause.Connect("button_up", this, nameof(handleContinue));
        levels.Connect("button_up", this, nameof(handleLevelSelect));
        exit.Connect("button_up", this, nameof(handleExit));
    }
    
    public void handleContinue() {
        GetTree().Paused = false;
        Visible = false;
    }
    public void handleLevelSelect() {
        GetTree().ChangeSceneTo(ResourceLoader.Load<PackedScene>("res://scenes/levels.tscn"));
        GetTree().Paused = false;
    }
    public void handleExit() {
        GetTree().Quit();
    }

}