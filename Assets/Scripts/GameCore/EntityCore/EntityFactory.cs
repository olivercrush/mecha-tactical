using UnityEngine;

public class EntityFactory {
    // SINGLETON PART
    private static EntityFactory _instance;

    private EntityFactory() {
        _entityPrefab = Resources.Load<GameObject>("Prefabs/Entity");
    }

    public static EntityFactory GetInstance() {
        if (_instance == null) {
            _instance = new EntityFactory();
        }
        return _instance;
    }

    // LOGIC PART
    private GameObject _entityPrefab;

    public GameObject CreateDebugBuilding(Vector2 coords, PlayerColor color) {
        GameObject entity = GameObject.Instantiate(_entityPrefab);
        entity.AddComponent<DebugBuilding>().Spawn(coords, color);
        //entity.GetComponent<DebugBuilding>().Spawn(coords, color);
        return entity;
    }
}
