using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cell : MonoBehaviour
{
    protected Vector2 _position;
    protected CellType _type;

    public Vector2 GetPosition() { return _position; }
    public CellType GetCellType() { return _type; }

    public abstract void Activate(CellType type);
    protected abstract void SelectThisCell();
    public abstract void DeselectThisCell();
}
