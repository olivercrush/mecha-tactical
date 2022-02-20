using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityCore : GameplayCore {

    private List<Entity> _entities;

    public EntityCore() { }

    public List<Entity> GetEntities() { 
        return _entities; 
    }

    public void AddEntity(Entity entity) {
        _entities.Add(entity);
    }

    public Entity GetEntityAtPosition(Vector2 position) {
        foreach (Entity entity in _entities) {
            if (entity.GetCoordinates() == position) return entity;
        }
        Debug.LogError("EntityCore - GetEntityAtPosition : No entity was found at " + position.ToString());
        return null;
    }

    public void HandleUpdate(Update update) {
        
    }
}
