using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public int _mapViewSize = 10;
    private (int, int) _mapViewFocus;

    private int[,] _mapData;
    private GameObject[,] _mapCells;

    void Start()
    {
        LoadLevel("000");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SetMapViewFocus((_mapViewFocus.Item1 - 1, _mapViewFocus.Item2));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            SetMapViewFocus((_mapViewFocus.Item1 + 1, _mapViewFocus.Item2));
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            SetMapViewFocus((_mapViewFocus.Item1, _mapViewFocus.Item2 - 1));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            SetMapViewFocus((_mapViewFocus.Item1, _mapViewFocus.Item2 + 1));
        }
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
                GameObject cell = Instantiate(ColorDebugPrefabFactory.GetMapCell(_mapData[y, x]), new Vector3(transform.position.x + displayX, transform.position.y + _mapViewSize - displayY, 1), Quaternion.identity, transform);
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
