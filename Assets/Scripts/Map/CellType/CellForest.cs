using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellForest : CellType {
    public CellForest() : base() {
        _id = 1;
        _name = "Forest";
        _spritePath = "Sprites/Forest32x32";
        _debugColor = new Color(0, 1, 0);
    }
}
