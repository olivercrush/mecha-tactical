using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGraphics : MonoBehaviour {
    private Vector2 _coords;
    private CellType _type;

    public CellType GetCellType() { return _type; }

    private void OnMouseEnter() {
        transform.GetComponentInParent<MapGraphics>()._cursor.SelectCell(this);
    }

    private void OnMouseExit() {
        transform.GetComponentInParent<MapGraphics>()._cursor.DeselectCell();
    }

    private void OnMouseDown() {
        transform.GetComponentInParent<MapGraphics>()._selector.SelectCell(this);
    }

    public void SetCoords(Vector2 coords) {
        _coords = coords;
    }

    public void SetType(CellType type) {
        _type = type;
        if (TryGetComponent<SpriteRenderer>(out SpriteRenderer sr)) {
            sr.sprite = _type.GetSprite();
        }
    }
}