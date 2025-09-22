using System;
using DG.Tweening;
using UnityEngine;

public class RotationState : IDisposable
{
    private Transform _target;
    private float _time;
    private Vector3 _left;
    private Vector3 _right;
    private Sequence _sequence;
    private float _halfAngle;
        
    public RotationState(Transform target, float time, float halfAngle)
    {
        _target = target;
        _time = time;
        _halfAngle = halfAngle;
    }

    public void Init()
    {
        _left = new Vector3(_target.localEulerAngles.x, -_halfAngle, _target.localEulerAngles.z);
        _right = new Vector3(_target.localEulerAngles.x, _halfAngle, _target.localEulerAngles.z);
    }

    public void Enable()
    {
        var value = Calculate(NormalizeAngle(_target.localEulerAngles.y));
        _target.DOLocalRotate(_left, _time * value).SetEase(Ease.Linear).OnComplete(StartLoop).SetId(0);
    }

    public void Disable()
    {
        DOTween.Kill(0);
        DOTween.Kill(1);
    }

    private float NormalizeAngle(float angle)
    {
        while (angle > 180f) angle -= 360f;
        while (angle < -180f) angle += 360f;
        return angle;
    }
    
    private float Calculate(float angle)
    {
        float all = _halfAngle * 2;
        angle += _halfAngle;
        return angle / all;
    }

    private void StartLoop()
    {
        var sequence = DOTween.Sequence();
        sequence.SetId(1);
        sequence.AppendInterval(2);
        sequence.Append(_target.DOLocalRotate(_right, _time)).SetEase(Ease.Linear);
        sequence.AppendInterval(2);
        
        sequence.Append(_target.DOLocalRotate(_left, _time)).SetEase(Ease.Linear);
        sequence.SetLoops(-1);
    }

    public void Dispose()
    {
        Disable();
    }
}
