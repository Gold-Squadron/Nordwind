using Godot;
using System;

public class Boat : KinematicBody2D {

    private const float MAX_SPEED = 40;
    private Main main;
    private Vector2 velocity;
    
    public override void _Ready() {
        main = GetNode<Main>("/root/Main");
        velocity = Vector2.Down * (float)(MAX_SPEED * 0.8f);
    }

    public override void _PhysicsProcess(float delta) {
        Vector2 dir = (main.windDir * 0.3f + velocity).Normalized();
        velocity = velocity.Normalized() * (MAX_SPEED);
        velocity += (dir);
        GD.Print(velocity);
        if (Input.IsPhysicalKeyPressed((int) KeyList.Space)) {
            return;
        }
        var collisionResult = MoveAndCollide(velocity * delta);
        if (collisionResult != null) {
            //GD.
            GetTree().ReloadCurrentScene();
        }
        
        Rotation = Main.getAngleInRadians(velocity);
    }

}