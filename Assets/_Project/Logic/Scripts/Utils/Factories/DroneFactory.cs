using UnityEngine;

public class DroneFactory : IEntityFactory<Drone>
{
    public int PoolSize { get; set; } = 10;

    private EntityPool<Drone> _pool;
    private GameObject _prefab;
    private Base _droneBase;
    private DroneConfigSO _droneConfig;

    public DroneFactory(DroneConfigSO droneConfig, Base droneBase)
    {
        _prefab = droneConfig.dronePrefab;
        _droneBase = droneBase;
        _droneConfig = droneConfig;
        _pool = new EntityPool<Drone>(_prefab, droneBase.transform, PoolSize);
    }

    public Drone GetEntity(Transform spawnPoint = null)
    {
        Drone drone = _pool.Get(spawnPoint);
        drone.Initialize(_droneBase, _droneConfig);
        return drone;
    }

    public void ReturnEntity(Drone drone)
    {
        _pool.Return(drone);
    }
}
