using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Entity
{
    protected int _buildingType;

    public Building(Vector2 coordinates, PlayerColor color) : base(coordinates, color) {
        _buildingType = 0;
    }
}