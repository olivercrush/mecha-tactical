using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : MonoBehaviour
{
    public float _xOrg;
    public float _yOrg;
    public float _scale;

    private MapCore _mapCore;

    public void CreateMap(int w, int h) {
        MapGenerator.GetInstance().SetParameters(_xOrg, _yOrg, _scale);
        if (_mapCore == null) _mapCore = new MapCore();
        _mapCore.CreateMap(w, h);
    }

    public int[,] GetMapPart(int x1, int y1, int x2, int y2) {
        return _mapCore.GetMap().GetCellGridPart(x1, y1, x2, y2);
    }
}

