using Godot;
using System;

public class Boat : RigidBody2D {

    private const float MAX_SPEED = 40;
    private Main main;
    private Vector2 velocity;
    
    public override void _Ready() {
        main = GetNode<Main>("/root/Main");
        velocity = Vector2.Zero;
    }

    public override void _Process(float delta) {
        GD.Print(velocity.Dot(main.windDir));
        velocity += (main.windDir * delta);
        velocity = velocity.LimitLength(MAX_SPEED);
        Position += velocity * delta;
        Rotation = main.getAngleInRadians();
    }

}