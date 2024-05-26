using Godot;
using System;

public class LevelLoadButton : Button  {
    [Export]
    public PackedScene scene;

    public void setLevel(int x) {
        scene = ResourceLoader.Load<PackedScene>("res://scenes/levels/level_" + x + ".tscn");
        Text = x.ToString();
    }
    
    public override void _Pressed() {
        GetTree().ChangeSceneTo(scene);
    }
}
