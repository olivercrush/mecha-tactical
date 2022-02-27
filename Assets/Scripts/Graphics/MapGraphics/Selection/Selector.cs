using System.Collections;
using UnityEngine;

public class Selector : MonoBehaviour {

    private const string SPRITE_PATH = "Sprites/Selector16x16";

    private CellGraphics _selectedCell;
    private Vector2 _selectedCellCoordinates;
    private SpriteRenderer _sr;

    void Start() {
        name = "Selector";
        _sr = gameObject.AddComponent<SpriteRenderer>();
        _sr.sprite = Resources.Load<Sprite>(SPRITE_PATH);

        _selectedCell = null;
        _selectedCellCoordinates = new Vector2(-1, -1);
        HideSelector();
    }

    public void SelectCell(CellGraphics cell) {
        if (cell == _selectedCell) {
            DeselectCell();
            GetComponentInParent<MapGraphics>()._cursor.SelectCell(cell);
        }
        else {
            // ON MAP
            _selectedCell = cell;
            //_selectedCellCoordinates = cell.GetCoordinates();
            GetComponentInParent<MapGraphics>()._cursor.DeselectCell();
            ShowSelector();

            // ON UI
            // ObjectFinder.GetUIManager().SetCellTypeText(_selectedCell.GetCellType().GetName());
            // ObjectFinder.GetUIManager().SetCellCoordinates(_selectedCell.GetCoordinates());
            // ObjectFinder.GetUIManager().SetCellSprite(_selectedCell.GetCellType().GetSprite());
        }
    }

    public void DeselectCell() {
        _selectedCell = null;
        _selectedCellCoordinates = new Vector2(-1, -1);
        HideSelector();

        // ON UI
        // ObjectFinder.GetUIManager().ClearCellSelection();
    }

    public void ShowSelector() {
        if (_selectedCell != null) {
            transform.position = _selectedCell.transform.position;
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);
            _sr.enabled = true;
        }
    }

    public void HideSelector() {
        _sr.enabled = false;
    }

    public CellGraphics GetSelectedCell() { return _selectedCell; }

    public Vector2 GetSelectedCellCoordinates() { return _selectedCellCoordinates; }
}
