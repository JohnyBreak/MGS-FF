using DG.Tweening;
using UnityEngine;

public class DoorView : MonoBehaviour
{
    [SerializeField] private Collider _collider;
    [SerializeField] private Transform _door;
    [SerializeField] private float _duration = 0.4f;
    public Collider Collider => _collider;
    
    private void Awake()
    {
        if (_collider == null)
        {
            Debug.LogError($"Door at position {transform.position} Has no collider");
            return;
        }

        _collider.isTrigger = true;
    }

    public void Open()
    {
        _door.DOLocalMoveX(1, _duration * (1 - _door.transform.localPosition.x));
    }

    public void Close()
    {
        _door.DOLocalMoveX(0, _duration * _door.transform.localPosition.x);
    }
}
