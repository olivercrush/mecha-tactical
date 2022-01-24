using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cell : MonoBehaviour {
    protected CellType _type;

    public CellType GetCellType() { return _type; }

    public SpriteRenderer GetSpriteRenderer() {
        if (TryGetComponent<SpriteRenderer>(out SpriteRenderer sr)) {
            return sr;
        }
        return null;
    }

    public virtual void SetType(CellType type) {
        _type = type;
    }

    protected abstract void SelectThisCell();
    public abstract void DeselectThisCell();
}
