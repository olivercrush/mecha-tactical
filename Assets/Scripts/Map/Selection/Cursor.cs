using System.Collections;
using UnityEngine;

public class Cursor : MonoBehaviour {

    void Start() {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void SelectCell(Cell cell) {
        if (cell != ObjectFinder.GetSelector().GetSelectedCell()) {
            transform.position = cell.transform.position;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void DeselectCell() {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}