using System;
using DG.Tweening;
using UnityEngine;

public class RotationState : MonoBehaviour
{
    [SerializeField] private float _time = 4;
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
        var value = Calculate(NormalizeAngle(transform.localEulerAngles.y));
        transform.DOLocalRotate(_left, _time * value).SetEase(Ease.Linear).OnComplete(StartLoop).SetId(0);
    }
    
    private float NormalizeAngle(float angle)
    {
        while (angle > 180f) angle -= 360f;
        while (angle < -180f) angle += 360f;
        return angle;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.LogError(Calculate(NormalizeAngle(transform.localEulerAngles.y)));
        }
    }

    private float Calculate(float angle)
    {
        float all = 65 * 2;
        angle += 65;
        return angle / all;
    }

    private void StartLoop()
    {
        var sequence = DOTween.Sequence();
        sequence.SetId(1);
        sequence.AppendInterval(2);
        sequence.Append(transform.DOLocalRotate(_right, _time)).SetEase(Ease.Linear);
        sequence.AppendInterval(2);
        
        sequence.Append(transform.DOLocalRotate(_left, _time)).SetEase(Ease.Linear);
        sequence.SetLoops(-1);
    }

    private void OnDisable()
    {
        DOTween.Kill(0);
        DOTween.Kill(1);
    }
}
