using System;
using Godot;
using Nordwind;

public class Target : Node2D {
    [Export] private float radius = 100;
    private Color color;
    [Export] public int type = 0;
    private Camera2D cam;
    private Line2D line;

    [Signal]
    delegate void all_boats_reached_target();


    public override void _Draw() {
        color.a = .5F;
        DrawCircle(Vector2.Zero, radius, color);
        color.a = 1F;
        DrawArc(Vector2.Zero, radius, 0, 7F, 100, color, 5F, true);
        //Vector2 offset = GetNode<Camera2D>("/root/Main/Follower/Camera2D").
        //DrawArc(Vector2.Down, 8, 0, 180, 100, color, 1f, true);
    }

    public override void _Ready() {
        GetNode<Sprite>("BuoyTint").Modulate = Util.getColor(type);
        color = Util.getColor(type);
        
        GetNode<CollisionShape2D>("Area2D/CollisionShape2D").Shape.Set("Radius", radius);

        Shape2D aShape = GetNode<CollisionShape2D>("Area2D/CollisionShape2D").Shape;
        CircleShape2D aCircleShape2D = (CircleShape2D)aShape;
        aCircleShape2D.Radius = radius * 100;

        GetNode("Area2D").Connect("body_entered", this, nameof(_on_Area2D_body_entered));
        GetNode("VisibilityNotifier2D").Connect("screen_entered", this, nameof(removeMarker));
        GetNode("VisibilityNotifier2D").Connect("screen_exited", this, nameof(addMarker));

        cam = GetNode<Camera2D>("/root/Main/Follower/Camera2D");
    }

    public override void _Process(float delta) {
        if(line == null) return;
        Vector2 m = (cam.Position - Position).Normalized();
        line.Rotation = m.Angle();
        m = new Vector2(-m.x, m.y);
        line.Position = cam.GetViewportRect().Size * 0.4f * m;
    }

    private void addMarker() {
        line = new Line2D();
        line.Points = new[] {new Vector2(0, 0), new Vector2(0, 20), new Vector2(20, 20)};
        line.Width = 4f;
        cam.AddChild(line);
        GD.Print("off_screen");
    }
    
    private void removeMarker() {
        if(line == null) return;
        cam.RemoveChild(line);
        line = null;
        GD.Print("on_screen");
    }

    private void _on_Area2D_body_entered(Node body) {
        GD.Print("BODY ENTERED!");
        if (body.GetType() == typeof(Boat)) {
            Boat boat = (Boat)body;
            if (boat.type == type) {
                boat.Deactivate();
                boat.SetPhysicsProcess(false);
                if (--Main.AcitveBoatCounter == 0) {
                    EmitSignal(nameof(all_boats_reached_target));
                }

                // GD.Print(Main.AcitveBoatCounter);
            }
        }
    }
}