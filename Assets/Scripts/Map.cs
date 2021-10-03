using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public int _mapViewSize = 10;
    private int _mapViewFocus;

    private int[,] _mapData;
    private GameObject[,] _mapCells;

    void Start()
    {
        LoadLevel("000");
    }

    void Update()
    {
        
    }

    private void LoadLevel(string name)
    {
        _mapData = FileUtils.ReadLevelFromFile(name);
        _mapCells = new GameObject[_mapData.GetLength(0), _mapData.GetLength(1)];

        DisplayMap();
    }

    private void DisplayMap()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int y = 0; y < _mapData.GetLength(0); y++)
        {
            for (int x = 0; x < _mapData.GetLength(1); x++)
            {
                GameObject cell = Instantiate(ColorDebugPrefabFactory.GetMapCell(_mapData[y, x]), new Vector3(transform.position.x + x, transform.position.y + (_mapData.GetLength(0) - y - 1), 1), Quaternion.identity, transform);
                cell.name = x + ":" + y + " - " + _mapData[y, x];
                _mapCells[y, x] = cell;
            }
        }
    }
}
