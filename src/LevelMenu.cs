using Godot;
using System;
using System.Diagnostics;

public class LevelMenu : Control {

	[Export] 
	public PackedScene btnPrefab;

	[Export] public int levels = 1;
	
	public override void _Ready() {
		HFlowContainer lvlContainer = GetNode<HFlowContainer>("lvls");
		for (int i = 1; i <= levels; i++) {
			LevelLoadButton btn = btnPrefab.Instance() as LevelLoadButton;
			Debug.Assert(btn != null, nameof(btn) + " != null");
			btn.setLevel(i);
			btn.RectMinSize = new Vector2(100, 100); 
			lvlContainer.AddChild(btn);
		}
	}
}
