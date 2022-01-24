using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellPlain : CellType {
    public CellPlain() : base() {
        _id = 0;
        _name = "Plain";
        _spritePath = "Sprites/Plain16x16";
        _debugColor = new Color(1, 1, 1);
    }
}
