using UnityEditor;
using UnityEngine;

public class EntityType : ScriptableObject {

    protected int _id;
    protected string _name;
    protected string _spritePath;

    private Sprite _sprite;

    protected EntityType() {
        _sprite = null;
    }

    public int GetId() { return _id; }
    public string GetName() { return _name; }
    public Sprite GetSprite() {
        if (_sprite == null) {
            _sprite = Resources.Load<Sprite>(_spritePath);
        }
        return _sprite;
    }
}
