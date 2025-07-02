using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _resourcePrefab;
    [SerializeField] private float _spawnDelay = 1f;
    [SerializeField] private int _poolSize;

    [SerializeField] private Vector2 _spawnArea = new Vector2(10, 10);
    [SerializeField] private int _spawnLimit;

    private IEntityFactory<Resource> _factory;
    private List<Resource> _activeResources = new List<Resource>();

    private void Start()
    {
        _factory = new ResourceFactory(_resourcePrefab, transform)
        {
            PoolSize = _poolSize
        };

        StartCoroutine(SpawnRoutine());
    }

    private void OnDestroy()
    {
        StopAllCoroutines();

        foreach(var resource in _activeResources)
        {
            if(resource != null)
            {
                resource.OnCollected -= () => HandleResourceCollected(resource);
            }
        }
    }

    private IEnumerator SpawnRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(_spawnDelay);

            TrySpawnRecource();
        }
    }

    private void TrySpawnRecource()
    {
        if(_activeResources.Count >= _spawnLimit) 
            return;

        Vector3 spawnPosition = new Vector3(
            Random.Range(-_spawnArea.x, _spawnArea.x),
            0.5f,
            Random.Range(-_spawnArea.y, _spawnArea.y)
            );

        var resource = _factory.GetEntity();

        SetupResource(resource, spawnPosition);
    }

    private void SetupResource(Resource resource, Vector3 position)
    {
        resource.transform.position = position;
        resource.ResetState();

        resource.OnCollected -= () => HandleResourceCollected(resource);
        resource.OnCollected += () => HandleResourceCollected(resource);

        _activeResources.Add(resource);
    }

    private void HandleResourceCollected(Resource resource)
    {
        if (resource == null && !_activeResources.Contains(resource))
            return;

        ReleaseResource(resource);
    }

    private void ReleaseResource(Resource resource)
    {
        resource.OnCollected -= () => HandleResourceCollected(resource);
        _activeResources.Remove(resource);
        _factory.ReturnEntity(resource);

        //Debug.Log("Release resource");
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector3 center = transform.position + new Vector3(0f, 0.01f, 0f);
        Vector3 size = new Vector3(_spawnArea.x * 2, 0.1f, _spawnArea.y * 2);

        Gizmos.DrawWireCube(center, size);

        UnityEditor.Handles.Label(
            center + Vector3.up * 0.2f,
            $"Spawn area: {_spawnArea.x}x{_spawnArea.y}m",
            new GUIStyle { normal = new GUIStyleState { textColor = Color.green} }
            );
    }
#endif
}
