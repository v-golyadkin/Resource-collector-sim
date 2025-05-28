using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityPool<T> where T : MonoBehaviour
{
    private Queue<T> _pool = new Queue<T>();
    private GameObject _prefab;
    private Transform _parent;
    private bool _autoExpand;

    public EntityPool(GameObject prefab, Transform parent, int initialSize, bool autoExpand = true)
    {
        _prefab = prefab;
        _parent = parent;
        _autoExpand = autoExpand;

        PreparePool(initialSize);
    }

    private void PreparePool(int size)
    {
        for(int i = 0; i < size; i++)
        {
            AddEntityToPool();
        }
    }

    private T AddEntityToPool()
    {
        T entity = CreateNewEntity();
        entity.gameObject.SetActive(false);
        _pool.Enqueue(entity);
        return entity;
    }

    private T CreateNewEntity()
    {
        var obj = GameObject.Instantiate(_prefab, _parent);

        return obj.GetComponent<T>();
    }

    public T Get(Transform spawnPoint = null)
    {
        if(_pool.Count == 0)
        {
            if (_autoExpand)
            {
                AddEntityToPool();
            }
            else
            {
                throw new InvalidOperationException("Pool is empty and auto expand is disable");
            }
        }

        T entity = _pool.Dequeue();
        entity.transform.position = spawnPoint?.position ?? Vector3.zero;
        entity.gameObject.SetActive(true);
        return entity;
    }

    public void Return(T entity)
    {
        entity.gameObject.SetActive(false);
        _pool.Enqueue(entity);
    }
}
