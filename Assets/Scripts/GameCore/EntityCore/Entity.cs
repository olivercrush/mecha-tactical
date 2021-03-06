using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity
{
    protected Vector2 _coordinates;
    protected PlayerColor _color;

    public Vector2 GetCoordinates() { return _coordinates; }
    public PlayerColor GetColor() { return _color; }

    protected Entity(Vector2 coordinates, PlayerColor color) {
        _coordinates = coordinates;
        _color = color;
    }
}

public enum PlayerColor {
    RED,
    BLUE,
    WHITE
}
