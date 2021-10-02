using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private int[,] _mapData;

    void Start()
    {
        _mapData = FileUtils.ReadLevelFromFile("000");

        for (int y = 0; y < _mapData.GetLength(0); y++)
        {
            for (int x = 0; x < _mapData.GetLength(1); x++)
            {
                Instantiate(ColorDebugPrefabFactory.GetMapCell(_mapData[y, x]), new Vector3(x, y, 1), Quaternion.identity, transform);
            }
        }
    }

    void Update()
    {
        
    }
}
