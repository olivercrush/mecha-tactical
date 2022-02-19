using UnityEditor;
using UnityEngine;

public class Update {

    private UpdateType _type;
    private Vector2 _origin;
    private Vector2 _target;

    public Update(UpdateType type, Vector2 origin) {
        _type = type;
        _origin = origin;
        _target = origin;
    }

    public Update(UpdateType type, Vector2 origin, Vector2 target) {
        _type = type;
        _origin = origin;
        _target = target;
    }

    public UpdateType GetUpdateType() { return _type; }
    public Vector2 GetOrigin() { return _origin; }
    public Vector2 GetTarget() { return _target; }

}

