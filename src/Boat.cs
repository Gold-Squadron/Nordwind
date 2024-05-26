using Godot;
using System;
using System.Data.SqlClient;
using Nordwind;

public class Boat : KinematicBody2D {

    [Export] private float MAX_SPEED = 40;
    private Main main;
    [Export] private Vector2 velocity;

    [Export] public int type = 0;
    
    public bool active = true;
    private float alpha = 1;
    
    public override void _Ready() {
        main = GetNode<Main>("/root/Main");
        velocity = main.windDir;// * (float)(MAX_SPEED * 0.3f);
        Main.AcitveBoatCounter++;

        GetNode<Sprite>("Sprite/Sail").Modulate = Util.getColor(type).LinearInterpolate(Colors.Beige, 0.45f);
    }

    public override void _Process(float delta) {
        if (!active && alpha > 0) {
            alpha -= 1F/100F;
            GetNode<Sprite>("Sprite").Modulate = new Color(1, 1, 1, alpha);
            Vector2 dir = (main.windDir * 0.3f + velocity).Normalized();
            velocity = velocity.Normalized() * MAX_SPEED + dir;
            var collisionResult = MoveAndCollide(velocity * delta);
        }
    }

    public override void _PhysicsProcess(float delta) {
        Vector2 dir = (main.windDir * 0.3f + velocity).Normalized();
        velocity = velocity.Normalized() * (MAX_SPEED);
        velocity += (dir);
        // GD.Print(velocity);
        if (Input.IsPhysicalKeyPressed((int) KeyList.Space)) {
            return;
        }
        var collisionResult = MoveAndCollide(velocity * delta);
        if (collisionResult != null) {
            Main.AcitveBoatCounter = 0;
            GetTree().ReloadCurrentScene();
        }
        
        Rotation = Main.getAngleInRadians(velocity);
    }

    public void Deactivate() {
        active = false;
        RemoveChild(GetNode<CollisionShape2D>("CollisionShape2D"));
    }

}