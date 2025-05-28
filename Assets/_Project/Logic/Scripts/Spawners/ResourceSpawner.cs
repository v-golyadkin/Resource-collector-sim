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
    private int _spawnCount = 0;

    private void Start()
    {
        _factory = new ResourceFactory(_resourcePrefab, transform)
        {
            PoolSize = _poolSize
        };

        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(_spawnDelay);

            if(_spawnCount < _spawnLimit)
            {
                SpawnResource();
            }
                
        }
    }

    private void SpawnResource()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(-_spawnArea.x, _spawnArea.x),
            0.5f,
            Random.Range(-_spawnArea.y, _spawnArea.y)
            );

        _factory.GetEntity().transform.position = spawnPosition;

        _spawnCount++;
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
