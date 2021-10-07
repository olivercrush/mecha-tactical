using System.Collections;
using UnityEngine;

public class Selector : MonoBehaviour {

    private Cell _selectedCell;
    private Vector2 _selectedCellCoordinates;

    void Start() {
        _selectedCellCoordinates = new Vector2(-1, -1);
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void SelectCell(Cell cell) {
        if (cell == _selectedCell) {
            DeselectCell();
            ObjectFinder.GetCursor().SelectCell(cell);
        }
        else {
            _selectedCell = cell;
            _selectedCellCoordinates = cell.GetCoordinates();
            transform.position = _selectedCell.transform.position;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            ObjectFinder.GetCursor().DeselectCell();
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void DeselectCell() {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public Cell GetSelectedCell() { return _selectedCell; }

    public Vector2 GetSelectedCellCoordinates() { return _selectedCellCoordinates; }
}
