using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellTypeWater : CellType {
    public CellTypeWater() : base() {
        _id = 2;
        _name = "Water";
        _spritePath = "Sprites/Water16x16";
        _debugColor = new Color(0, 0, 1);
    }
}
