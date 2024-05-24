using Godot;

public class Main : Node {

    [Export]
    public Vector2 windDir = Vector2.Up;

    public override void _Ready() {
        GD.Print("Main Level is initializing.");
        
    }

    public override void _Process(float delta) {
        handlePlayerInput(delta);
    }
    
    void handlePlayerInput(float delta) {
        var desiredDirection = Vector2.Zero;

        if (Input.IsActionPressed("ui_right")) {
            desiredDirection.x += 1;
        }

        if (Input.IsActionPressed("ui_left")) {
            desiredDirection.x -= 1;
        }

        if (Input.IsActionPressed("ui_down")) {
            desiredDirection.y += 1;
        }

        if (Input.IsActionPressed("ui_up")) {
            desiredDirection.y -= 1;
        }

        windDir += desiredDirection * delta;
        windDir = windDir.Normalized();
    }

    public float getAngleInRadians() {
        return new Vector2(-windDir.x, windDir.y).AngleTo(Vector2.Up);
    }
}