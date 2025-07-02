using System.Collections.Generic;
using UnityEngine;

public class DroneSpawner : MonoBehaviour
{
    [SerializeField] private DroneSpawnerConfigSO _droneSpawnerConfig;
    [SerializeField] private DroneConfigSO _droneConfig;
    [SerializeField] private Base _droneBase;

    private IEntityFactory<Drone> _factory;
    private List<Drone> _drones = new List<Drone>();

    private void Start()
    {
        _factory = new DroneFactory(_droneConfig, _droneBase)
        {
            PoolSize = _droneSpawnerConfig.dronePerFaction
        };

        SpawnDrones();
    }

    private void SpawnDrones()
    {
        for (int i = 0; i < _droneSpawnerConfig.dronePerFaction; i++)
        {
            SpawnDrone();
        }
    }

    private Drone SpawnDrone()
    {
        Drone drone = _factory.GetEntity(_droneBase.transform);
        _drones.Add(drone);
        return drone;
    }
}
