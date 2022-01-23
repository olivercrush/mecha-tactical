using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DisplayType {
    DEBUG,
    PROD
}

public class MapGraphics : MonoBehaviour {
    public DisplayType _displayType;

    public int _mapViewSize = 10;
    public float _mapSpeed = 0.5f;
    private Vector2 _mapViewFocus;
    private Vector2 _mapMovement;

    private int[,] _mapData;
    private Cell[,] _mapCells;

    void Start() {
        // LoadLevel("000");
        // _mapMovement = new Vector2(0, 0);
    }

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
        SetMapViewFocus(_mapViewFocus + _mapMovement);
    }

    public void Load() {
        DeleteAllCells();

        //_mapData = FileUtils.ReadMapFromFile(name);
        _mapData = ObjectFinder.GetMapGenerator().GenerateMap(new Vector2(30, 30));
        _mapCells = new Cell[_mapData.GetLength(0), _mapData.GetLength(1)];

        for (int y = 0; y < _mapCells.GetLength(0); y++) {
            for (int x = 0; x < _mapCells.GetLength(1); x++) {
                GameObject cell = CellPrefabFactory.GetInstance().CreateMapCell(_mapData[y, x], new Vector2(x, y), transform, _displayType);
                cell.name = "Cell (" + x + ":" + y + ") - " + cell.GetComponent<Cell>().GetCellType().GetName();
                _mapCells[y, x] = cell.GetComponent<Cell>();
            }
        }

        _mapMovement = new Vector2(0, 0);
        SetMapViewFocus(new Vector2(_mapData.GetLength(1) / 2, _mapData.GetLength(0) / 2));
    }

    private void DisplayMap() {
        /*foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }*/
        DeactivateAllCells();

        Selector selector = ObjectFinder.GetSelector();
        Vector2 selectedCellCoordinates = selector.GetSelectedCellCoordinates();
        selector.HideSelector();

        int displayY = 0;
        for (int y = (int)_mapViewFocus.y - _mapViewSize / 2; y < _mapViewFocus.y + _mapViewSize / 2; y++) {

            int displayX = 0;
            for (int x = (int)_mapViewFocus.x - _mapViewSize / 2; x < _mapViewFocus.x + _mapViewSize / 2; x++) {

                _mapCells[y, x].transform.position = new Vector3(transform.position.x + displayX, transform.position.y + _mapViewSize - displayY, 1);
                _mapCells[y, x].gameObject.SetActive(true);

                /*GameObject cell = CellPrefabFactory.GetInstance().CreateMapCell(_mapData[y, x], new Vector2(x, y), new Vector3(transform.position.x + displayX, transform.position.y + _mapViewSize - displayY, 1), transform, _displayType);
                cell.name = x + ":" + y + " - " + _mapData[y, x];
                _mapCells[y, x] = cell;*/


                if (x == selectedCellCoordinates.x && y == selectedCellCoordinates.y) {
                    // Debug.Log("Showing (" + x + ":" + y + ")");
                    selector.ShowSelector();
                }

                displayX++;
            }
            displayY++;
        }
    }

    private void SetMapViewFocus(Vector2 mapViewFocus) {
        if (mapViewFocus.x - _mapViewSize / 2 < 0) { Debug.LogError("Map.cs : Trying to set a map view focus going out of bonds (west) -> " + mapViewFocus); }
        else if (mapViewFocus.x + _mapViewSize / 2 > _mapData.GetLength(1)) { Debug.LogError("Map.cs : Trying to set a map view focus going out of bonds (east) -> " + mapViewFocus); }
        else if (mapViewFocus.y - _mapViewSize / 2 < 0) { Debug.LogError("Map.cs : Trying to set a map view focus going out of bonds (north) -> " + mapViewFocus); }
        else if (mapViewFocus.y + _mapViewSize / 2 > _mapData.GetLength(0)) { Debug.LogError("Map.cs : Trying to set a map view focus going out of bonds (south) -> " + mapViewFocus); }
        else {
            _mapViewFocus = mapViewFocus;
            DisplayMap();
        }
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
