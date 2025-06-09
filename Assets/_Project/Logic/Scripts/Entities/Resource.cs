using System;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public bool IsCollected { get; private set; }
    public bool IsMarked { get; private set; }

    public event Action OnCollected;

    private void OnEnable()
    {
        ResetState();
    }

    private void OnDisable()
    {
        OnCollected = null;
    }

    public void Initialize()
    {
        IsCollected = false;
        IsMarked = false;
    }

    public void Collect()
    {
        if (IsCollected || !gameObject.activeInHierarchy) 
            return;
        
        IsCollected = true;
        OnCollected?.Invoke();
    }

    public void MarkAsCollected()
    {
        IsMarked = true;
        //IsCollected = false;
    }

    public void ResetState()
    {
        IsCollected = false;
        IsMarked = false;
    }

    public bool TryToClaim()
    {
        if (IsMarked) return false;

        MarkAsCollected();
        //IsCollected = true;
        return true;
    }
}
