using System.Collections;
using UniRx;
using UnityEngine;

public class SurveillanceCamera : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Collider _collider;
    [SerializeField] private Transform _rotator;
    
    private CompositeDisposable _disposable = new();
    private Detection _detection;
    private RotationState _rotationState;
    private LookAtState _lookAtState;
    private Coroutine _resetRoutine;
    
    private void Awake()
    {
        _lookAtState = new LookAtState(_rotator, 65, 3);
        _detection = new Detection(_layerMask, _collider);
        _rotationState = new RotationState(_rotator, 4f, 65);
        _detection.Target
            .Skip(1)
            .Subscribe(OnTarget)
            .AddTo(_disposable);
    }

    private IEnumerator Start()
    {
        yield return null;
        _detection.Init();
        _rotationState.Init();
        _rotationState.Enable();
    }

    private void OnTarget(Transform target)
    {
        if (target == null)
        {
            if (_resetRoutine != null)
            {
                StopCoroutine(_resetRoutine);
            }

            _resetRoutine = StartCoroutine(ResetRoutine());
            return;
        }
        _rotationState.Disable();
        _lookAtState.Enable(target);
    }

    private IEnumerator ResetRoutine()
    {
        yield return new WaitForSeconds(3);
        _rotationState.Enable();
        _lookAtState.Disable();
    }

    // get hit boxes
    // invoke break event

    private void OnDestroy()
    {
        _detection?.Dispose();
        _rotationState?.Dispose();
    }
}
