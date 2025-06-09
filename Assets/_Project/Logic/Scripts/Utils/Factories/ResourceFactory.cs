using UnityEngine;

public class ResourceFactory : IEntityFactory<Resource>
{
    public int PoolSize { get; set; } = 10;

    private EntityPool<Resource> _pool;

    public ResourceFactory(GameObject prefab, Transform parent)
    {
        _pool = new EntityPool<Resource>(prefab, parent, PoolSize, false);
    }

    public Resource GetEntity(Transform spawnPoint = null)
    {
        Resource resource = _pool.Get(spawnPoint);
        return resource;
    }

    public void ReturnEntity(Resource entity)
    {
        _pool.Return(entity);
    }
}
