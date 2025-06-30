using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/DroneConfig", fileName = "DroneConfig")]
public class DroneConfigSO : ScriptableObject
{
    [SerializeField] public GameObject dronePrefab;

    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float collectTime = 2f;
    [SerializeField] public float findResourceRadius = 10f;

}
