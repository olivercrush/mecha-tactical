using UnityEditor;
using UnityEngine;

public class Update {

    private UpdateType _type;
    private Vector2 _origin;
    private Vector2 _target;

    private string[] _args;

    public Update(UpdateType type, Vector2 origin, string[] args) {
        _type = type;
        _origin = origin;
        _target = origin;
        _args = args;
    }

    public Update(UpdateType type, Vector2 origin, Vector2 target, string[] args) {
        _type = type;
        _origin = origin;
        _target = target;
        _args = args;
    }

    public UpdateType GetUpdateType() { return _type; }
    public Vector2 GetOrigin() { return _origin; }
    public Vector2 GetTarget() { return _target; }

    public string[] GetArgs() { return _args; }

}

