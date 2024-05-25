using Godot;

public class Main : Node {

    [Export]
    public Vector2 windDir = Vector2.Up;
    [Export]
    public float windSpeed = 80;

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

        windDir += windSpeed * desiredDirection * delta;
        windDir = windDir.Normalized() * windSpeed;
    }

    public float getAngleInRadians() {
        return new Vector2(-windDir.x, windDir.y).AngleTo(Vector2.Up);
    }
}