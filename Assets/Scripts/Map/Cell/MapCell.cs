using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCell : Cell {
    public override void Activate(CellType type) {
        base.Activate(type);
        SpriteRenderer sr = GetSpriteRenderer();
        if (sr != null) {
            //Debug.Log(_type.GetSprite());
            sr.sprite = _type.GetSprite();
        }
    }

    public override void DeselectThisCell() {
        throw new System.NotImplementedException();
    }

    protected override void SelectThisCell() {
        throw new System.NotImplementedException();
    }
}
