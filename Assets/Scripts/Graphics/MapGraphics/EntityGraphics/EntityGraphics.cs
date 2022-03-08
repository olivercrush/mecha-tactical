using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityGraphics : MonoBehaviour {

    private EntityType _type;
    private PlayerColor _color;

    public EntityType GetCellType() { return _type; }

    public void SetPlayerColor(PlayerColor color) {
        _color = color;

        if (TryGetComponent<SpriteRenderer>(out SpriteRenderer sr)) {
            switch (_color) {
                case PlayerColor.RED:
                    sr.color = Color.red;
                    break;

                case PlayerColor.BLUE:
                    sr.color = Color.blue;
                    break;

                default:
                    sr.color = Color.white;
                    break;
            }
        }
    }

    public void SetType(EntityType type) {
        _type = type;
        if (TryGetComponent<SpriteRenderer>(out SpriteRenderer sr)) {
            sr.sprite = _type.GetSprite();
        }
    }
}