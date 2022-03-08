using UnityEditor;
using UnityEngine;

public class EntityGraphicsPrefabFactory {
    // SINGLETON PART
    private EntityGraphicsPrefabFactory() {
        _entityTypeDebug = ScriptableObject.CreateInstance<EntityTypeDebug>();
    }

    private static EntityGraphicsPrefabFactory _instance;

    public static EntityGraphicsPrefabFactory GetInstance() {
        if (_instance == null) {
            _instance = new EntityGraphicsPrefabFactory();
        }
        return _instance;
    }

    // LOGIC PART
    private EntityTypeDebug _entityTypeDebug;

    public GameObject CreateEntity(Transform parent, int type, PlayerColor color) {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Entity");
        GameObject entity = GameObject.Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity, parent);
        
        EntityGraphics entityGraphics = entity.GetComponent<EntityGraphics>();
        entityGraphics.SetType(GetEntityType(type));
        entityGraphics.SetPlayerColor(color);

        return entity;
    }

    private EntityType GetEntityType(int type) {
        switch (type) {
            default:
                return _entityTypeDebug;
        }
    }
}
