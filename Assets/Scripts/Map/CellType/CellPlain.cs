using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellPlain : CellType
{
    public CellPlain()
    {
        _id = 0;
        _name = "Plain";
        _sprite = null;
        _debugColor = new Color(1, 1, 1);
    }
}
