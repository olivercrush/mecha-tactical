using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityCore : GameplayCore {

    private List<Entity> _entities;

    public EntityCore() { 
        _entities = new List<Entity>(); 
    }

    public List<Entity> GetEntities() { 
        return _entities; 
    }

    public Entity GetEntityAtPosition(Vector2 position) {
        foreach (Entity entity in _entities) {
            if (entity.GetCoordinates() == position) return entity;
        }
        Debug.LogError("EntityCore - GetEntityAtPosition : No entity was found at " + position.ToString());
        return null;
    }

    public void HandleUpdate(Update update) {
        switch (update.GetUpdateType()) {
            case UpdateType.SPAWN_BUILDING:
                _entities.Add(new Building(update.GetOrigin(), GetPlayerColorFromArgs(int.Parse(update.GetArgs()[1]))));
                break;
        }
    }

    // TODO : create a Update -> Entity translator
    private PlayerColor GetPlayerColorFromArgs(int playerColor) {
        switch (playerColor) {
            case 1:
                return PlayerColor.RED;

            case 2:
                return PlayerColor.BLUE;

            default:
                return PlayerColor.WHITE;
        }
    }
}
