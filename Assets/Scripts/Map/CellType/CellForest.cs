using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellForest : CellType
{
    public CellForest()
    {
        _id = 1;
        _name = "Forest";
        _sprite = null;
        _debugColor = new Color(0, 1, 0);
    }
}
