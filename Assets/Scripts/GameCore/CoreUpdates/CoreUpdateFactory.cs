using UnityEditor;
using UnityEngine;

public class CoreUpdateFactory {

    public static Update CreateGenerateMapUpdate(int w, int h) {
        string[] args = new string[] { w.ToString(), h.ToString() };
        return new Update(UpdateType.GENERATE_MAP, new Vector2(0, 0), args);
    }

    public static Update CreateSpawnBuildingUpdate(int x, int y, int type) {
        string[] args = new string[1] { type.ToString() };
        return new Update(UpdateType.SPAWN_BUILDING, new Vector2(x, y), args);
    }

}
