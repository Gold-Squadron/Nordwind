using Godot;
using System;

public class MovingObject : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    [Export] private float speed = 20;
    private Main main;
    Vector2 velocity;
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        main = GetNode<Main>("/root/Main");
        velocity = new Vector2((float)(new Random().NextDouble()-.5), (float)(new Random().NextDouble()-.5)) * (float)(speed * 0.8f);

    }

    public override void _PhysicsProcess(float delta) {
        Vector2 dir = (main.windDir * 0.3f + velocity).Normalized();
        velocity = velocity.Normalized() * speed + dir;
        var collisionResult = MoveAndCollide(velocity * delta);
        
        if (collisionResult != null) {
            GD.Print("HALLO");
            
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
