using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityGraphics : MonoBehaviour {

    private Vector2 _coords;
    private EntityType _type;

    public EntityType GetCellType() { return _type; }

    public void SetCoords(Vector2 coords) {
        _coords = coords;
    }

    public void SetType(EntityType type) {
        _type = type;
        if (TryGetComponent<SpriteRenderer>(out SpriteRenderer sr)) {
            sr.sprite = _type.GetSprite();
        }
    }
}