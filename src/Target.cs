using Godot;
using System;
using System.Security.Policy;

public class Target : Node2D {
    [Export] private float radius = 100;
    [Export] private Color color = Colors.Blue;
    [Export] public int type;

    [Signal]
    delegate void all_boats_reached_target();

    public override void _Draw() {
        color.a = .5F;
        DrawCircle(Vector2.Zero, radius, color);
        color.a = 1F;
        DrawArc(Vector2.Zero, radius, 0, 7F, 100, color, 5F, true);
    }

    public override void _Ready() {
        GetNode<CollisionShape2D>("Area2D/CollisionShape2D").Shape.Set("Radius", radius);

        Shape2D aShape = GetNode<CollisionShape2D>("Area2D/CollisionShape2D").Shape;
        CircleShape2D aCircleShape2D = (CircleShape2D)aShape;
        aCircleShape2D.Radius = radius * 30;

        GetNode("Area2D").Connect("body_entered", this, nameof(_on_Area2D_body_entered));
    }

    private void _on_Area2D_body_entered(Node body) {
        GD.Print("BODY ENTERED!");
        if (body.GetType() == typeof(Boat)) {
            Boat boat = (Boat)body;
            boat.Deactivate();
            boat.SetPhysicsProcess(false);
            if (--Main.AcitveBoatCounter == 0) {
                EmitSignal(nameof(all_boats_reached_target));
            }

            GD.Print(Main.AcitveBoatCounter);
        }
    }
}