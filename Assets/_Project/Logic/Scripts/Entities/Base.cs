using System;
using UnityEngine;

public class Base : MonoBehaviour
{
    private int _collectResourceCount = 0;

    public Action<int> OnCollectResource;

    public void AddResource(int amount)
    {
        _collectResourceCount += amount;
        OnCollectResource?.Invoke(_collectResourceCount);
    }
}
