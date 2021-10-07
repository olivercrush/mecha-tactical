using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public int _mapViewSize = 10;
    public float _mapSpeed = 0.5f;
    private (int, int) _mapViewFocus;
    private (int, int) _mapMovement;

    private int[,] _mapData;
    private GameObject[,] _mapCells;

    void Start()
    {
        LoadLevel("000");
        _mapMovement = (0, 0);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.Q)) { CancelInvoke("MoveMap"); }

        if (Input.GetKeyDown(KeyCode.A))
        {
            CancelInvoke("MoveMap");
            _mapMovement = (-1, 0);
            InvokeRepeating("MoveMap", 0.0f, _mapSpeed);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            CancelInvoke("MoveMap");
            _mapMovement = (1, 0);
            InvokeRepeating("MoveMap", 0.0f, _mapSpeed);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            CancelInvoke("MoveMap");
            _mapMovement = (0, -1);
            InvokeRepeating("MoveMap", 0.0f, _mapSpeed);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            CancelInvoke("MoveMap");
            _mapMovement = (0, 1);
            InvokeRepeating("MoveMap", 0.0f, _mapSpeed);
        }
    }

    private void MoveMap()
    {
        SetMapViewFocus((_mapViewFocus.Item1 + _mapMovement.Item1, _mapViewFocus.Item2 + _mapMovement.Item2));
    }

    private void LoadLevel(string name)
    {
        _mapData = FileUtils.ReadLevelFromFile(name);
        _mapCells = new GameObject[_mapData.GetLength(0), _mapData.GetLength(1)];
        _mapViewFocus = (10, 10);

        SetMapViewFocus((5, 5));
    }

    private void DisplayMap()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        int displayY = 0;
        for (int y = _mapViewFocus.Item2 - _mapViewSize / 2; y < _mapViewFocus.Item2 + _mapViewSize / 2; y++)
        {
            int displayX = 0;
            for (int x = _mapViewFocus.Item1 - _mapViewSize / 2; x < _mapViewFocus.Item1 + _mapViewSize / 2; x++)
            {
                GameObject cell = ColorDebugPrefabFactory.CreateMapCell(_mapData[y, x], new Vector3(transform.position.x + displayX, transform.position.y + _mapViewSize - displayY, 1), transform);
                cell.name = x + ":" + y + " - " + _mapData[y, x];
                _mapCells[y, x] = cell;
                displayX++;
            }
            displayY++;
        }
    }

    private void SetMapViewFocus((int, int) mapViewFocus)
    {
        if (mapViewFocus.Item1 - _mapViewSize / 2 < 0) { Debug.LogError("Map.cs : Trying to set a map view focus going out of bonds (west) -> " + mapViewFocus); }
        else if (mapViewFocus.Item1 + _mapViewSize / 2 > _mapData.GetLength(1)) { Debug.LogError("Map.cs : Trying to set a map view focus going out of bonds (east) -> " + mapViewFocus); }
        else if (mapViewFocus.Item2 - _mapViewSize / 2 < 0) { Debug.LogError("Map.cs : Trying to set a map view focus going out of bonds (north) -> " + mapViewFocus); }
        else if (mapViewFocus.Item2 + _mapViewSize / 2 > _mapData.GetLength(0)) { Debug.LogError("Map.cs : Trying to set a map view focus going out of bonds (south) -> " + mapViewFocus); }
        else 
        { 
            _mapViewFocus = mapViewFocus;
            DisplayMap();
        }
    }
}
