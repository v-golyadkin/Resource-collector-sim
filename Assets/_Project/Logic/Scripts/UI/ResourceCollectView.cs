using TMPro;
using UnityEngine;

public class ResourceCollectView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _resourceCollectText;
    [SerializeField] private Base _base;

    private void OnEnable()
    {
        _base.OnCollectResource += UpdateResourceCollectText;
    }

    private void OnDisable()
    {
        _base.OnCollectResource -= UpdateResourceCollectText;
    }

    private void UpdateResourceCollectText(int resourceCollectValue)
    {
        _resourceCollectText.text = $"Resource collect: {resourceCollectValue}";
    }
}
