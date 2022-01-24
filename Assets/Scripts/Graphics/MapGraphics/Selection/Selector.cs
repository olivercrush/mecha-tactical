using System.Collections;
using UnityEngine;

public class Selector : MonoBehaviour {

    private Cell _selectedCell;
    private Vector2 _selectedCellCoordinates;

    void Start() {
        _selectedCell = null;
        _selectedCellCoordinates = new Vector2(-1, -1);
        HideSelector();
    }

    public void SelectCell(Cell cell) {
        if (cell == _selectedCell) {
            DeselectCell();
            ObjectFinder.GetCursor().SelectCell(cell);
        }
        else {
            // ON MAP
            _selectedCell = cell;
            //_selectedCellCoordinates = cell.GetCoordinates();
            ObjectFinder.GetCursor().DeselectCell();
            ShowSelector();

            // ON UI
            ObjectFinder.GetUIManager().SetCellTypeText(_selectedCell.GetCellType().GetName());
            //ObjectFinder.GetUIManager().SetCellCoordinates(_selectedCell.GetCoordinates());
            ObjectFinder.GetUIManager().SetCellSprite(_selectedCell.GetCellType().GetSprite());
        }
    }

    public void DeselectCell() {
        _selectedCell = null;
        _selectedCellCoordinates = new Vector2(-1, -1);
        HideSelector();

        // ON UI
        ObjectFinder.GetUIManager().ClearCellSelection();
    }

    public void ShowSelector() {
        if (_selectedCell != null) {
            transform.position = _selectedCell.transform.position;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void HideSelector() {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public Cell GetSelectedCell() { return _selectedCell; }

    public Vector2 GetSelectedCellCoordinates() { return _selectedCellCoordinates; }
}
