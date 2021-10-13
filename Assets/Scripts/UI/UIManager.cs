using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public Text _cellType;
    public Text _cellCoordinates;
    public SpriteRenderer _cellSprite;

    public void SetCellTypeText(string text) {
        _cellType.text = text;
    }

    public void SetCellCoordinates(Vector2 coordinates) {
        _cellCoordinates.text = "X: " + coordinates.x.ToString() + " / Y: " + coordinates.y.ToString();
    }

    public void SetCellSprite(Sprite sprite) {
        _cellSprite.sprite = sprite;
    }

    public void ClearCellSelection() {
        _cellType.text = "none";
        _cellCoordinates.text = "X: - / Y: -";
        _cellSprite.sprite = null;
    }
}
