using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCell : Cell {

    private void OnMouseEnter() {
        SelectThisCell();
    }

    private void OnMouseExit() {
        DeselectThisCell();
    }

    private void OnMouseDown() {
        ObjectFinder.GetSelector().SelectCell(this);
    }

    public override void Activate(Vector2 position, CellType type) {
        base.Activate(position, type);
        SpriteRenderer sr = GetSpriteRenderer();
        if (sr != null) {
            //Debug.Log(_type.GetSprite());
            sr.sprite = _type.GetSprite();
        }
    }

    public override void DeselectThisCell() {
        ObjectFinder.GetCursor().DeselectCell();
    }

    protected override void SelectThisCell() {
        ObjectFinder.GetCursor().SelectCell(this);
    }
}
