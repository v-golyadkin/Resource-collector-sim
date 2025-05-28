using UnityEngine;

public class Resource : MonoBehaviour
{
    public bool IsCollected {  get; private set; }

    public void MarkAsCollected()
    {
        IsCollected = true;
    }

    public void ResetState()
    {
        IsCollected = false;
    }
}
