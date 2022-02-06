using System.Collections;
using UnityEngine;

public class Cursor : MonoBehaviour {

    private SpriteRenderer _sr;

    public void Start() {
        _sr = gameObject.GetComponent<SpriteRenderer>();
        _sr.enabled = false;
    }

    public void SelectCell(Cell cell) {
        transform.position = cell.transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        _sr.enabled = true;
    }

    public void DeselectCell() {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}