using System.Collections;
using UnityEngine;

public class MapCore {

    private MapGenerator _mapGenerator;
    private Map _map;

    public MapCore() {
        _mapGenerator = new MapGenerator();
    }

    public Map CreateMap(int w, int h) {
        _map = new Map(_mapGenerator.GenerateMap(new Vector2(w, h)));
        return _map;
    }

    public Map GetMap() { return _map; }

}
