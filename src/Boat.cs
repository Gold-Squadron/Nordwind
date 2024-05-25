using Godot;
using System;

public class Boat : KinematicBody2D {

    private const float MAX_SPEED = 40;
    private Main main;
    private Vector2 velocity;
    
    public override void _Ready() {
        main = GetNode<Main>("/root/Main");
        velocity = Vector2.Zero;
    }

    public override void _PhysicsProcess(float delta) {
        GD.Print(velocity.Dot(main.windDir));
        velocity += (main.windDir * delta);
        velocity = velocity.LimitLength(MAX_SPEED);
        var collisionResult = MoveAndCollide(velocity * delta);
        if (collisionResult != null) {
            //GD.
            GetTree().ReloadCurrentScene();
        }
        Rotation = main.getAngleInRadians();
    }

}