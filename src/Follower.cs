using Godot;
using System;
using System.Collections.Generic;

public class Follower : Node2D {
    private List<Boat> boats = new List<Boat>();
    
    public override void _Ready() {
        foreach (var sibling in GetParent().GetChildren()) {
            if (sibling is Boat boat && boat.type != -1) {
                boats.Add(boat);
            }
        }
    }

    private Vector2 CalculateMiddle() {
        Vector2 sum = Vector2.Zero;
        List<Boat> toBeRemoved = new List<Boat>();
        foreach (var boat in boats) {
            if (boat.active) {
                sum += boat.Position;
            } else { 
                toBeRemoved.Add(boat);
            }
        }

        foreach (var boat in toBeRemoved) {
            boats.Remove(boat);
        }
        
        return boats.Count != 0 ? sum / boats.Count: Position;
    }

    
    
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta) {
      Vector2 middle = CalculateMiddle();
      foreach (var boat in boats) {
          Vector2 distance = (Position - boat.Position).Abs();

          if (distance.x > 400 || distance.y > 300) {
              Position = middle.LinearInterpolate(Position, 0.15f);
              return;
          }
      }
  }
  
  

}
