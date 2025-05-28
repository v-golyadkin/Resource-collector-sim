using UnityEngine;

public class DroneFactory : IEntityFactory<Drone>
{
    public int PoolSize { get; set; } = 10;

    private EntityPool<Drone> _pool;
    private GameObject _prefab;
    private Transform _homeBase;

    public DroneFactory(GameObject prefab, Transform homeBase)
    {
        _prefab = prefab;
        _homeBase = homeBase;
        _pool = new EntityPool<Drone>(prefab, homeBase, PoolSize);
    }

    public Drone GetEntity(Transform spawnPoint = null)
    {
        Drone drone = _pool.Get(spawnPoint);
        drone.Initialize(_homeBase);
        return drone;
    }

    public void ReturnEntity(Drone drone)
    {
        _pool.Return(drone);
    }
}
