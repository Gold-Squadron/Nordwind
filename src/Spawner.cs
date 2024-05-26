using Godot;
using System;

public class Spawner : Node2D {

    [Export]
    public PackedScene element;

    [Export]
    public int delayInMs;
    
    [Export]
    public int distance;
    [Export]
    private Vector2 velocity;
    
    
    public float lastSpawn = -100;
    
    public override void _Ready() {
        Boat boat = element.Instance() as Boat;
        //boat.Position = Position;
        boat.velocity = velocity;
        boat.type = -1;
        boat.maxDistance = distance;
        AddChild(boat);
        lastSpawn = Time.GetTicksMsec();
    }

    public override void _Process(float delta) {
        if (lastSpawn + delayInMs < Time.GetTicksMsec()) {
            
            Boat boat = element.Instance() as Boat;
            //boat.Position = Position;
            boat.velocity = velocity;
            boat.type = -1;
            boat.maxDistance = distance;
            AddChild(boat);
            lastSpawn = Time.GetTicksMsec();
        }
    }

}