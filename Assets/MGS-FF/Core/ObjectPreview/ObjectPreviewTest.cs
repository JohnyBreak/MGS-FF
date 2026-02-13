using UnityEngine;

public class ObjectPreviewTest : MonoBehaviour
{
    [SerializeField] private PreviewView _view;
    [SerializeField] private ObjectRenderParent _objectRenderParent;
    [SerializeField] private PreviewInteractable _interactable;
    
    private ObjectPreviewSystem _system;
    
    private void Start()
    {
        _system = new(_view, _objectRenderParent);
        _interactable.Init(_system);
    }
}
