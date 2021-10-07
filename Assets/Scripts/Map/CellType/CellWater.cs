using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CellWater : CellType {
    public CellWater() : base() {
        _id = 2;
        _name = "Water";
        _spritePath = "Sprites/Water16x16";
        _debugColor = new Color(0, 0, 1);
    }
}
