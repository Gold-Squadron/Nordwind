using Godot;

public class TheLimit : Node2D {

    private Area2D area;
    private CollisionPolygon2D shape;
    private Vector2[] points;
    
    public override void _Ready() {
        area = GetNode<Area2D>("Area2D");
        shape = area.GetNode<CollisionPolygon2D>("CollisionPolygon2D");
        area.Connect("body_exited", this, nameof(_on_Area2D_area_exited));

        points = new Vector2[shape.Polygon.Length+1];
        for (int i = 0; i < shape.Polygon.Length; i++) {
            points[i] = shape.Polygon[i % shape.Polygon.Length];
        }
    }
    
    public override void _Draw() {
        DrawPolyline(points, Colors.Red, 3f, true);
    }
    
    public void _on_Area2D_area_exited(CollisionObject a) {
        if (a.GetType() != typeof(Boat)) {
            return;
        }

        Main.AcitveBoatCounter = 0;
        GetTree().ReloadCurrentScene();
    }

}