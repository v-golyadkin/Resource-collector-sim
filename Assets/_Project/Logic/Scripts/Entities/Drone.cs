using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public enum DroneState { Searching, MovingToResource, Collecting, ReturningToBase }


public class Drone : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _collectionTime = 2f;

    [SerializeField] private DroneState _currentState;
    [SerializeField] private Resource _targetResource;
    [SerializeField] private int _resourceCollected;

    [SerializeField] private float _findResourceRadius = 10f;

    private Transform _homeBase;
    private Base _droneBase;
    private float _collectionTimer;

    public void Initialize(Transform homeBase, Base droneBase)
    {
        _droneBase = droneBase;
        _homeBase = homeBase;
        SetState(DroneState.Searching);
    }

    private void Update()
    {
        switch(_currentState)
        {
            case DroneState.Searching:
                FindResource();
                break;
            case DroneState.MovingToResource:
                MoveTo(_targetResource.transform.position);
                break;
            case DroneState.Collecting:
                CollectResource();
                break;
            case DroneState.ReturningToBase:
                StopAllCoroutines();
                MoveTo(_droneBase.transform.position);
                break;
        }
    }

    private void SetState(DroneState newState)
    {
        _currentState = newState;
    }

    private void FindResource()
    {
        Collider[] nearbyResources = Physics.OverlapSphere(
            transform.position,
            _findResourceRadius,
            LayerMask.GetMask("Resource")
            );

        foreach (var collider in nearbyResources)
        {
            Resource resource = collider.GetComponent<Resource>();
            if (resource != null && resource.TryToClaim())
            {
                ClaimResource(resource);
                return;
            }
        }

        if (Random.Range(0, 100) < 5)
        {
            Vector3 randomDir = new Vector3(
                Random.Range(-1f, 1f),
                0,
                Random.Range(-1f, 1f)
            ).normalized;
            transform.position += randomDir * _moveSpeed * Time.deltaTime;
        }
    }

    private void ClaimResource(Resource resource)
    {
        _targetResource = resource;
        SetState(DroneState.MovingToResource);
    }

    private void MoveTo(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            _moveSpeed * Time.deltaTime
            );

        if(Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            if(_currentState == DroneState.MovingToResource)
            {
                SetState(DroneState.Collecting);
            }
            else if(_currentState == DroneState.ReturningToBase)
            {
                SetState(DroneState.Searching);
                DeliverResource();
            }
        }
    }

    private void DeliverResource()
    {
        _droneBase.AddResource(1);
    }

    private void CollectResource()
    {
        StartCoroutine(CollectResourceRoutine());    
    }

    private IEnumerator CollectResourceRoutine()
    {
        yield return new WaitForSeconds(_collectionTime);

        if(_targetResource != null && _targetResource.gameObject.activeInHierarchy)
        {
            _targetResource.Collect();
        }
        

        SetState(DroneState.ReturningToBase);
    }
}
