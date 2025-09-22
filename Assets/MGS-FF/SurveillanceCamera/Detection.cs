using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Detection : IDisposable
{
    private CompositeDisposable _disposable = new();
    private LayerMask _layerMask;
    private Collider _collider;
    private Coroutine _resetRoutine;
    public IReactiveProperty<Transform> Target { get; private set; } = new ReactiveProperty<Transform>(null);
    
    public Detection(LayerMask layerMask, Collider collider)
    {
        _layerMask = layerMask;
        _collider = collider;
    }

    public void Init()
    {
        _collider
            .OnTriggerEnterAsObservable()
            .Subscribe(OnTriggerEnter)
            .AddTo(_disposable);
        _collider
            .OnTriggerExitAsObservable()
            .Subscribe(OnTriggerExit)
            .AddTo(_disposable);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((_layerMask.value & (1 << other.transform.gameObject.layer)) <= 0)
        {
            return;
        }

        Target.Value = other.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if ((_layerMask.value & (1 << other.transform.gameObject.layer)) <= 0)
        {
            return;
        }
        
        if (Target.Value != other.transform)
        {
            return;
        }
        
        Target.Value = null;
    }

    public void Dispose()
    {
        _disposable?.Dispose();
    }
}
