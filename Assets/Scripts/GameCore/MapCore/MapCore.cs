using System.Collections;
using UnityEngine;

public class MapCore {

    private MapGenerator _mapGenerator;
    private Map _map;

    public MapCore() {
        _mapGenerator = new MapGenerator();
        _map = new Map(_mapGenerator.GenerateMap(new Vector2(20, 20)));
    }

    public Map GetMap() { return _map; }

}
