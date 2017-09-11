
using FullSerializer;
using System;
using System.Collections.Generic;
using System.Linq;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class EntityManager
{
    private List<Entity> mEntities = new List<Entity>();

    public T CreateEntity<T>() where T : Entity, new()
    {
        T instance = Activator.CreateInstance<T>();
        instance.name = instance.GetType().ToString();
        instance.OnStart();
        this.AddEntity((Entity)instance);
        return instance;
    }

    public void AddEntity(Entity inEntity)
    {
        this.mEntities.Add(inEntity);
    }

    public void DestroyEntity(Entity inEntity)
    {
        inEntity.OnDestory();
        this.RemoveEntity(inEntity);
    }

    public void RemoveEntity(Entity inEntity)
    {
        this.mEntities.Remove(inEntity);
    }
}
