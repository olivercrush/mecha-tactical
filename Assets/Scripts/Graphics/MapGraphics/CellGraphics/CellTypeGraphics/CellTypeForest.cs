using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellTypeForest : CellType {
    public CellTypeForest() : base() {
        _id = 1;
        _name = "Forest";
        _spritePath = "Sprites/Forest16x16";
        _debugColor = new Color(0, 1, 0);
    }
}
