using Godot;

public class Compass : Sprite {

    private Main main;
    private Sprite foreground;

    public override void _Ready() {
        foreground = GetNode("arrow") as Sprite;
        main = GetNode<Main>("/root/Main");
         // GD.Print(Position);
    }


    public override void _Process(float delta) {
        Position = new Vector2(Texture.GetWidth() * Scale.x / 2f + 15, GetViewportRect().Size.y - Texture.GetHeight() * Scale.y / 2f - 15);
        // Position = new Vector2(-GetViewport().Size.x/2 + Texture.GetWidth() * Scale.x/2F, GetViewport().Size.y/2 - Texture.GetHeight() * Scale.y/2F);
        // GD.Print(GetViewportRect().Size);
        // setWindDirection(new Vector2((float)GD.RandRange(-1f, 1f), (float) GD.RandRange(-1, 1)));
        foreground.Rotation = main.getAngleInRadians();
    }
}

