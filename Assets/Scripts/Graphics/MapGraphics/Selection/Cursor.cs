using System.Collections;
using UnityEngine;

public class Cursor : MonoBehaviour {

    private SpriteRenderer _sr;

    public void Start() {
        name = "Cursor";
        _sr = gameObject.GetComponent<SpriteRenderer>();
        _sr.enabled = false;
    }

    public void SelectCell(Cell cell) {
        if (GetComponentInParent<MapGraphics>()._selector.GetSelectedCell() == cell) {
            DeselectCell();
            return;
        }

        transform.position = cell.transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        _sr.enabled = true;
    }

    public void DeselectCell() {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}