using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBuilding : Entity
{
    public override void Spawn(Vector2 coordinates, PlayerColor color) {
        base.Spawn(coordinates, color);

        if (TryGetComponent<SpriteRenderer>(out SpriteRenderer sr)) {
            sr.sprite = Resources.Load<Sprite>("Sprites/Building16x16");
            
            switch (color) {
                case PlayerColor.BLUE:
                    sr.color = Color.blue;
                    break;

                case PlayerColor.RED:
                    sr.color = Color.red;
                    break;

                default:
                    sr.color = Color.white;
                    break;
            }
        }
    }
}
