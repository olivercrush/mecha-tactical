using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CellType : ScriptableObject
{
    protected int _id;
    protected string _name;
    protected Sprite _sprite;
    protected Color _debugColor;

    public int GetId() { return _id; }
    public string GetName() { return _name; }
    public Sprite GetSprite() { return _sprite; }
    public Color GetDebugColor() { return _debugColor; }
}
