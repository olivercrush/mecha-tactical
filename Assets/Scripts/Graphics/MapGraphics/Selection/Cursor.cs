using System.Collections;
using UnityEngine;

public class Cursor : MonoBehaviour {

    private const string SPRITE_PATH = "Sprites/Cursor18x18";

    private SpriteRenderer _sr;

    public void Start() {
        name = "Cursor";
        _sr = gameObject.AddComponent<SpriteRenderer>();
        _sr.sprite = Resources.Load<Sprite>(SPRITE_PATH);
        _sr.enabled = false;
    }

    public void SelectCell(Cell cell) {
        transform.position = cell.transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        _sr.enabled = true;

        /*if (cell != ObjectFinder.GetSelector().GetSelectedCell()) {
            transform.position = cell.transform.position;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            GetComponent<SpriteRenderer>().enabled = true;
        }*/
    }

    public void DeselectCell() {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}