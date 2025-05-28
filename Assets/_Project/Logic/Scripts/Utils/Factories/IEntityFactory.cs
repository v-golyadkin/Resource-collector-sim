using UnityEngine;

public interface IEntityFactory<T> where T : MonoBehaviour
{
    T GetEntity(Transform spawnPoint = null);
    void ReturnEntity(T entity);
    int PoolSize { get; set; }
}
