using System;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public bool IsCollected {  get; private set; }

    public event Action OnCollected;

    public void OnResourceCollected()
    {
        OnCollected?.Invoke();
    }

    public void MarkAsCollected()
    {
        IsCollected = true;
    }

    public void ResetState()
    {
        IsCollected = false;
    }

    public bool TryToClaim()
    {
        if (IsCollected)
        {
            return false;
        }

        MarkAsCollected();
        return true;
    }
}
