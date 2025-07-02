using UnityEngine;

[CreateAssetMenu(menuName ="Configs/DroneSpawnerConfig", fileName ="DroneSpawnerConfig")]
public class DroneSpawnerConfigSO : ScriptableObject
{
    [SerializeField] public DroneConfigSO droneConfig;
    [SerializeField] public int dronePerFaction;
}
