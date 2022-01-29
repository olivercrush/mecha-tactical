using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CellType : ScriptableObject {
    protected int _id;
    protected string _name;
    protected string _spritePath;
    protected Color _debugColor;

    private Sprite _sprite;

    protected CellType() {
        _sprite = null;
    }

    public int GetId() { return _id; }
    public string GetName() { return _name; }
    public Sprite GetSprite() {
        if (_sprite == null) {
            _sprite = Resources.Load<Sprite>(_spritePath);
        }
        return _sprite; 
    }
    public Color GetDebugColor() { return _debugColor; }
}