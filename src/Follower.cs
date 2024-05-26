using Godot;
using System;
using System.Collections.Generic;

public class Follower : Node2D {
    private List<Boat> boats = new List<Boat>();
    
    public override void _Ready() {
        foreach (var sibling in GetParent().GetChildren()) {
            if (sibling.GetType() == typeof(Boat)) {
                boats.Add((Boat)sibling);
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
        
        return sum / boats.Count;
    }

    
    
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta) {
      Position = CalculateMiddle();
  }
  
  

}
