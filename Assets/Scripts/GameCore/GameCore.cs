using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : MonoBehaviour
{
    public float _xOrg;
    public float _yOrg;
    public float _scale;

    private MapCore _mapCore;
    private EntityCore _entityCore;

    private void Start() {
        MapGenerator.GetInstance().SetParameters(_xOrg, _yOrg, _scale);
        _mapCore = new MapCore();
        _entityCore = new EntityCore();
    }

    public void PropagateUpdate(Update update) {
        _mapCore.HandleUpdate(update);
        _entityCore.HandleUpdate(update);
    }

    public void CreateMap(int w, int h) {
        MapGenerator.GetInstance().SetParameters(_xOrg, _yOrg, _scale);
        if (_mapCore == null) _mapCore = new MapCore();
        _mapCore.CreateMap(w, h);
    }

    public int[,] GetMap() {
        return _mapCore.GetMap().GetCellGrid();
    }

    public List<Entity> GetEntities() {
        return _entityCore.GetEntities();
    }
}

