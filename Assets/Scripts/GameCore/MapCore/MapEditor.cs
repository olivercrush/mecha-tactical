using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Map))]
public class MapEditor : Editor {

    /*public override void OnInspectorGUI() {
        // base.OnInspectorGUI();
        DrawDefaultInspector();

        Map map = (Map) target;
        if (GUILayout.Button("Generate Map")) {
            map.Load();
        }
    }*/

}
