using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGraphics : MonoBehaviour {

    private float _mapSpeed = 0.5f;
    private int _mapViewSize = 20;
    private Vector2 _mapView;
    private Vector2 _mapMovement;
    
    private Cell[,] _mapCells;
    public Cursor _cursor;
    private Selector _selector;

    public void Start() {
        GameObject cursorObject = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Cursor"));
        cursorObject.transform.SetParent(transform);
        _cursor = cursorObject.GetComponent<Cursor>();

        /*GameObject selectorObject = new GameObject();
        selectorObject.transform.SetParent(transform);
        _selector = selectorObject.AddComponent<Selector>();*/
    }

    public void Load() {
        DeleteAllCells();
        _mapCells = new Cell[_mapViewSize, _mapViewSize];

        for (int y = 0; y < _mapCells.GetLength(0); y++) {
            for (int x = 0; x < _mapCells.GetLength(1); x++) {
                GameObject cell = CellPrefabFactory.GetInstance().CreateMapCell(transform);
                cell.name = "Cell (" + x + ":" + y + ")";
                _mapCells[y, x] = cell.GetComponent<Cell>();
                _mapCells[y, x].transform.position = new Vector3(transform.position.x + x, transform.position.y + _mapViewSize - y, 1);
                _mapCells[y, x].SetCoords(new Vector2(x, y));
            }
        }

        _mapMovement = new Vector2(0, 0);
        SetMapView(new Vector2(10, 10));
    } 

    private void DisplayMap() {
        // Selector selector = ObjectFinder.GetSelector();
        // Vector2 selectedCellCoordinates = selector.GetSelectedCellCoordinates();
        // selector.HideSelector();

        int[,] mapParts = GetComponentInParent<GraphicsManager>()._gameCore.GetMap();

        for (int y = 0; y < mapParts.GetLength(0); y++) {
            for (int x = 0; x < mapParts.GetLength(1); x++) {

                _mapCells[y, x].SetType(CellTypeHolder.GetCellTypeInstance(mapParts[y, x]));

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

    private void DeleteAllCells() {
        for (int i = transform.childCount - 1; i >= 0; i--) {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    private static class CellTypeHolder {
        private static CellPlain CELL_PLAIN = ScriptableObject.CreateInstance<CellPlain>();
        private static CellForest CELL_FOREST = ScriptableObject.CreateInstance<CellForest>();
        private static CellWater CELL_WATER = ScriptableObject.CreateInstance<CellWater>();

        public static CellType GetCellTypeInstance(int cellType) {
            switch (cellType) {
                case 0:
                    return CELL_PLAIN;

                case 2:
                    return CELL_FOREST;

                case 3:
                    return CELL_WATER;

                default:
                    return CELL_PLAIN;
            }
        }
    }
}
