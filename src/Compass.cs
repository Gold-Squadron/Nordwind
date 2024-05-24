using Godot;

public class Compass : Sprite {
    
    private Sprite foreground;

    public override void _Ready() {
        foreground = GetNode("arrow") as Sprite;
        Position = new Vector2(Texture.GetWidth() / 2f, GetViewportRect().Size.y - Texture.GetHeight() / 2f);
    }

    public void setWindDirection(Vector2 windDir) {
        foreground.Rotation = windDir.AngleTo(Vector2.Up);
    }

    public override void _Process(float delta) {
        //setWindDirection(new Vector2((float)GD.RandRange(-1f, 1f), (float) GD.RandRange(-1, 1)));
        setWindDirection(GetNode<Main>("/root/Main").windDir);
    }
}