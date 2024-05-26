using Godot;
using System;
using System.Data.SqlClient;
using Nordwind;

public class Boat : KinematicBody2D {

    [Export] private float MAX_SPEED = 40;
    private Main main;
    [Export] public Vector2 velocity;
    private Vector2 initialPosition;

    [Export] public int type = 0;
    [Export] public int maxDistance = 600;
    
    public bool active = true;
    private float alpha = 1;
    
    public override void _Ready() {
        main = GetNode<Main>("/root/Main");
        GetNode<Sprite>("Sprite/Sail").Modulate = Util.getColor(type).LinearInterpolate(Colors.Beige, 0.45f);
        if (type == -1) { // AI Mode
            initialPosition = Position;
            return;
        }

        
        Main.AcitveBoatCounter++;
        velocity = main.windDir;// * (float)(MAX_SPEED * 0.3f);
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
        var collisionResult = MoveAndCollide(velocity * delta);
        Rotation = Main.getAngleInRadians(velocity);
        
        if (type == -1) { // AI Mode
            if (Position.DistanceSquaredTo(initialPosition) > maxDistance * maxDistance) {
                GetParent().RemoveChild(this);
            }
            if (collisionResult != null) {
                if (collisionResult.Collider is Boat boat && boat.type != -1) {
                    Main.AcitveBoatCounter = 0;
                    GetTree().ReloadCurrentScene();
                } else {
                    if (collisionResult.Collider is StaticBody2D b && b.Name.Contains("Cloud")) {
                        return;
                    }
                    GetParent().RemoveChild(this);
                }
            }
            return;
        }
        Vector2 dir = (main.windDir * 0.3f + velocity).Normalized();
        velocity = velocity.Normalized() * (MAX_SPEED);
        velocity += (dir);
        // GD.Print(velocity);
        if (Input.IsPhysicalKeyPressed((int) KeyList.Space)) {
            return;
        }
        
        if (collisionResult != null) {
            Main.AcitveBoatCounter = 0;
            GetTree().ReloadCurrentScene();
        }
        
    }

    public void Deactivate() {
        active = false;
        RemoveChild(GetNode<CollisionShape2D>("CollisionShape2D"));
    }

}