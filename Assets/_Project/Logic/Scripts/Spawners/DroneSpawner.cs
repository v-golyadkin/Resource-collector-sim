using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroneSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _dronePrefab;
    [SerializeField] private Transform _homeBase;
    [SerializeField] private int _dronePerFaction;

    [SerializeField] private Color _factionColor;
    [SerializeField] private Text _factionName;

    private IEntityFactory<Drone> _factory;
    private List<Drone> _drones = new List<Drone>();

    private void Start()
    {
        _factory = new DroneFactory(_dronePrefab, _homeBase)
        {
            PoolSize = _dronePerFaction
        };

        SpawnDrones();
    }

    private void SpawnDrones()
    {
        for (int i = 0; i < _dronePerFaction; i++)
        {
            SpawnDrone();
        }
    }

    private Drone SpawnDrone()
    {
        Drone drone = _factory.GetEntity(_homeBase);
        _drones.Add(drone);
        return drone;
    }
}
