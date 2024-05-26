using Godot;
using System;

public class PauseMenu : Control {

    private Button unpause;
    private Button levels;
    private Button exit;
    
    [Export] private PackedScene levelScreen;

    public override void _Ready() {
        unpause = GetNode<Button>("VBoxContainer/continue");
        levels = GetNode<Button>("VBoxContainer/levels");
        exit = GetNode<Button>("VBoxContainer/exit");
        
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
        GetTree().ChangeSceneTo(levelScreen);
    }
    public void handleExit() {
        GetTree().Quit();
    }

}