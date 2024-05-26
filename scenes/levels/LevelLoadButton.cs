using Godot;
using System;

public class LevelLoadButton : Button  {
    [Export]
    public PackedScene scene;

    private int cnt;

    public void setLevel(int x) {
        scene = ResourceLoader.Load<PackedScene>("res://scenes/levels/Level_" + x + ".tscn");
        Text = x.ToString();
        cnt = x;
    }
    
    public override void _Pressed() {
        Global.CurrentLevel = cnt;
        GetTree().ChangeSceneTo(scene);
    }
}
