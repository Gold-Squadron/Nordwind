using Godot;
using System;
using System.Text;

public class LevelEnd : CanvasLayer {
    private Main main;
    private float alpha = 0;
    private float time = 0;

    public override void _Ready() {
        main = GetNode<Main>("/root/Main");

        Button mainMenu = GetNode<Button>("CenterContainer/VBoxContainer/MainMenu");
        Button nextLevel = GetNode<Button>("CenterContainer/VBoxContainer/NextLevel");

        nextLevel.GrabFocus();

        mainMenu.Connect("button_up", this, nameof(handleMainMenu));
        nextLevel.Connect("button_up", this, nameof(handleNextLevel));

        GetNode<CenterContainer>("CenterContainer").Modulate = new Color(1, 1, 1, alpha);
    }

    public void handleMainMenu() {
        GetTree().ChangeSceneTo(ResourceLoader.Load<PackedScene>("res://scenes/levels.tscn"));
    }

    public void handleNextLevel() {
        

        if (ResourceLoader.Exists("res://scenes/levels/level_" + (Global.CurrentLevel+1) + ".tscn")) {
            Global.CurrentLevel += 1;
            GD.Print(Global.CurrentLevel);
            GD.Print("res://scenes/levels/level_" + (Global.CurrentLevel+1) + ".tscn");
            GetTree().ChangeSceneTo(ResourceLoader.Load<PackedScene>("res://scenes/levels/level_" + Global.CurrentLevel + ".tscn"));
        }
    }

    public override void _Process(float delta) {
        if (Visible) {
            time += delta;
            if (time > 1 && alpha < 1) {
                alpha += 1F / 100F;
                GetNode<CenterContainer>("CenterContainer").Modulate = new Color(1, 1, 1, alpha);
            }
        }
    }
}