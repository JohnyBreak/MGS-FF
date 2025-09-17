using DG.Tweening;
using UnityEngine;

public class RotationState : MonoBehaviour
{
    private Vector3 _localRotation;
    private Vector3 _left;
    private Vector3 _right;
    private Sequence _sequence;

    private void Awake()
    {
        _localRotation = transform.localRotation.eulerAngles;
        _left = new Vector3(_localRotation.x, -65, _localRotation.z);
        _right = new Vector3(_localRotation.x, 65, _localRotation.z);
    }

    private void OnEnable()
    {
        transform.DOLocalRotate(_left, 2).SetEase(Ease.Linear).OnComplete(StartLoop).SetId(0);
    }

    private void StartLoop()
    {
        var sequence = DOTween.Sequence();
        sequence.SetId(1);
        sequence.AppendInterval(2);
        sequence.Append(transform.DOLocalRotate(_right, 4)).SetEase(Ease.Linear);
        sequence.AppendInterval(2);
        
        sequence.Append(transform.DOLocalRotate(_left, 4)).SetEase(Ease.Linear);
        sequence.SetLoops(-1);
    }

    private void OnDisable()
    {
        DOTween.Kill(0);
        DOTween.Kill(1);
    }
}
