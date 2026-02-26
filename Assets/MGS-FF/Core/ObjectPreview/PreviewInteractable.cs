using InteractSystem;
using UnityEngine;

public class PreviewInteractable : MonoBehaviour, IInteractable
{
    private ObjectPreviewSystem _system;
    
    public void Init(ObjectPreviewSystem system)
    {
        _system = system;
    }

    public void Interact()
    {
        _system.Preview(gameObject);
    }
}
