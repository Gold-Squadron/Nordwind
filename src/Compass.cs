using Godot;

public class Compass : Sprite {

    private Main main;
    private Sprite foreground;

    public override void _Ready() {
        foreground = GetNode("arrow") as Sprite;
        main = GetNode<Main>("/root/Main");
        Position = new Vector2(Texture.GetWidth() * Scale.x / 2f + 15, GetViewportRect().Size.y - Texture.GetHeight() * Scale.y / 2f - 15);
         // GD.Print(Position);
    }


    public override void _Process(float delta) {
        //setWindDirection(new Vector2((float)GD.RandRange(-1f, 1f), (float) GD.RandRange(-1, 1)));
        foreground.Rotation = main.getAngleInRadians();
    }
}