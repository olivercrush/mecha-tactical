using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cell : MonoBehaviour {
    protected Vector2 _coordinates;
    protected CellType _type;

    public Vector2 GetCoordinates() { return _coordinates; }
    public CellType GetCellType() { return _type; }

    public SpriteRenderer GetSpriteRenderer() {
        if (TryGetComponent<SpriteRenderer>(out SpriteRenderer sr)) {
            return sr;
        }

        Debug.LogError("Cell.cs : Cell (" + _coordinates.x + ":" + _coordinates.y + ") can't return SpriteRenderer (probably because it has not been given one)");
        return null;
    }

    public virtual void Activate(Vector2 coordinates, CellType type) {
        _coordinates = coordinates;
        _type = type;
    }

    protected abstract void SelectThisCell();
    public abstract void DeselectThisCell();
}
