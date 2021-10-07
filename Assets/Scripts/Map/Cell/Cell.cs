using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cell : MonoBehaviour {
    protected Vector2 _position;
    protected CellType _type;

    public Vector2 GetPosition() { return _position; }
    public CellType GetCellType() { return _type; }

    public SpriteRenderer GetSpriteRenderer() {
        if (TryGetComponent<SpriteRenderer>(out SpriteRenderer sr)) {
            return sr;
        }

        Debug.LogError("Cell.cs : Cell (" + _position.x + ":" + _position.y + ") can't return SpriteRenderer (probably because it has not been given one)");
        return null;
    }

    public virtual void Activate(CellType type) {
        _type = type;
    }

    protected abstract void SelectThisCell();
    public abstract void DeselectThisCell();
}
