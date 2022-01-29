using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {
    private CellType _type;

    public CellType GetCellType() { return _type; }

    public virtual void SetType(CellType type) {
        _type = type;
        if (TryGetComponent<SpriteRenderer>(out SpriteRenderer sr)) {
            sr.sprite = _type.GetSprite();
        }
    }

    protected void SelectThisCell() {

    }

    public void DeselectThisCell() {

    }
}