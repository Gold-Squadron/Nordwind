using System.Collections.Generic;
using System.Linq;
using Godot;

public class Main : Node {

    [Export] public Vector2 windDir = Vector2.Down;
    private Vector2 latestWindDir;
    
    private Queue<Vector2> lastDirections;
    private ulong lastCalc = 0;
    
    [Export]
    public float windSpeed = 80;
    
    public static int AcitveBoatCounter = 0;

    private Control pauseMenu;

    public override void _Ready() {
        GD.Print("Main Level is initializing.");

        latestWindDir = windDir;
        GetNode("Target").Connect("all_boats_reached_target", this, nameof(_on_Target_all_boats_reached_target));
        
        lastDirections = new Queue<Vector2>(10);
        for (int i = 0; i < 10; i++) {
            lastDirections.Enqueue(latestWindDir * 40);
        }

        pauseMenu = ResourceLoader.Load<PackedScene>("res://scenes/PauseMenu.tscn").Instance<Control>();
        AddChild(pauseMenu);
    }

    public override void _Process(float delta) {
        if (Input.IsActionPressed("ui_cancel")) {
            pauseMenu.Visible = true;
            GetTree().Paused = true;
        }
        
        handlePlayerInput(delta);
    }
    
    void handlePlayerInput(float delta) {
        var desiredDirection = Vector2.Zero;

        if (Input.IsActionPressed("ui_right") || Input.IsActionPressed("move_right")) {
            desiredDirection.x += 1;
        }

        if (Input.IsActionPressed("ui_left") || Input.IsActionPressed("move_left")) {
            desiredDirection.x -= 1;
        }

        if (Input.IsActionPressed("ui_down") || Input.IsActionPressed("move_down")) {
            desiredDirection.y += 1;
        }

        if (Input.IsActionPressed("ui_up") || Input.IsActionPressed("move_up")) {
            desiredDirection.y -= 1;
        }

        //latestWindDir += windSpeed * desiredDirection * delta;
        //GD.Print(latestWindDir, windDir);
        latestWindDir = (latestWindDir.Normalized() + desiredDirection * delta * 5) * windSpeed;
        if (lastCalc + 50 < Time.GetTicksMsec()) {
            windDir = lastDirections.Dequeue();
            lastDirections.Enqueue(new Vector2(latestWindDir));
            lastCalc = Time.GetTicksMsec();
            // GD.Print(lastCalc);
        }
    }

    public float getAngleInRadians() {
        return getAngleInRadians(latestWindDir);
    }
    
    public static float getAngleInRadians(Vector2 vec) {
        return new Vector2(-vec.x, vec.y).AngleTo(Vector2.Up);
    }

    private void _on_Target_all_boats_reached_target() {
        GD.Print("YOU DID IT!");
    }

}