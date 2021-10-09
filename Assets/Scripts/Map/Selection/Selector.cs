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
            _selectedCell = cell;
            _selectedCellCoordinates = cell.GetCoordinates();
            ObjectFinder.GetCursor().DeselectCell();
            ShowSelector();
        }
    }

    public void DeselectCell() {
        _selectedCell = null;
        _selectedCellCoordinates = new Vector2(-1, -1);
        HideSelector();
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
