using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DisplayType {
    DEBUG,
    PROD
}

public class MapGraphics : MonoBehaviour {
    public DisplayType _displayType = DisplayType.PROD;

    private float _mapSpeed = 0.5f;
    private int _mapViewSize = 20;
    private Vector2 _mapView;
    private Vector2 _mapMovement;

    private Cell[,] _mapCells;

    void Update() {
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.Q)) { CancelInvoke("MoveMap"); }

        if (Input.GetKeyDown(KeyCode.A)) {
            CancelInvoke("MoveMap");
            _mapMovement = new Vector2(-1, 0);
            InvokeRepeating("MoveMap", 0.0f, _mapSpeed);
        }
        else if (Input.GetKeyDown(KeyCode.D)) {
            CancelInvoke("MoveMap");
            _mapMovement = new Vector2(1, 0);
            InvokeRepeating("MoveMap", 0.0f, _mapSpeed);
        }

        if (Input.GetKeyDown(KeyCode.W)) {
            CancelInvoke("MoveMap");
            _mapMovement = new Vector2(0, -1);
            InvokeRepeating("MoveMap", 0.0f, _mapSpeed);
        }
        else if (Input.GetKeyDown(KeyCode.S)) {
            CancelInvoke("MoveMap");
            _mapMovement = new Vector2(0, 1);
            InvokeRepeating("MoveMap", 0.0f, _mapSpeed);
        }
    }

    private void MoveMap() {
        SetMapView(_mapView + _mapMovement);
    }

    public void Load() {
        DeleteAllCells();

        _mapCells = new Cell[_mapViewSize, _mapViewSize];

        for (int y = 0; y < _mapCells.GetLength(0); y++) {
            for (int x = 0; x < _mapCells.GetLength(1); x++) {
                GameObject cell = CellPrefabFactory.GetInstance().CreateMapCell(transform, _displayType);
                cell.name = "Cell (" + x + ":" + y + ")";
                _mapCells[y, x] = cell.GetComponent<Cell>();
                _mapCells[y, x].transform.position = new Vector3(transform.position.x + x, transform.position.y + _mapViewSize - y, 1);
            }
        }

        _mapMovement = new Vector2(0, 0);
        SetMapView(new Vector2(25, 25));
    }

    private void DisplayMap() {
        // Selector selector = ObjectFinder.GetSelector();
        // Vector2 selectedCellCoordinates = selector.GetSelectedCellCoordinates();
        // selector.HideSelector();

        int[,] mapParts = GetComponentInParent<GraphicsManager>()._gameCore.GetMapPart((int)_mapView.x - _mapViewSize / 2, (int)_mapView.y - _mapViewSize / 2, (int)_mapView.x + _mapViewSize / 2, (int)_mapView.y + _mapViewSize / 2);
        Debug.Log(mapParts.GetLength(0));

        for (int y = 0; y < mapParts.GetLength(0); y++) {
            for (int x = 0; x < mapParts.GetLength(1); x++) {

                _mapCells[y, x].SetType(CellPrefabFactory.GetInstance().GetCellTypeInstance(mapParts[y, x]));

                /*if (x == selectedCellCoordinates.x && y == selectedCellCoordinates.y) {
                    selector.ShowSelector();
                }*/
            }
        }
    }

    private void SetMapView(Vector2 mapView) {
        _mapView = mapView;
        DisplayMap();
    }

    private void DeactivateAllCells() {
        for (int y = 0; y < _mapCells.GetLength(0); y++) {
            for (int x = 0; x < _mapCells.GetLength(1); x++) {
                _mapCells[y, x].gameObject.SetActive(false);
            }
        }
    }

    private void DeleteAllCells() {
        for (int i = transform.childCount - 1; i >= 0; i--) {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}
