using Godot;

public class Compass : Sprite {

    private Main main;
    private Sprite foreground;

    public override void _Ready() {
        foreground = GetNode("arrow") as Sprite;
        main = GetNode<Main>("/root/Main");
        Position = new Vector2(Texture.GetWidth() / 2f, GetViewportRect().Size.y - Texture.GetHeight() / 2f);
    }


    public override void _Process(float delta) {
        //setWindDirection(new Vector2((float)GD.RandRange(-1f, 1f), (float) GD.RandRange(-1, 1)));
        foreground.Rotation = main.getAngleInRadians();
    }
}