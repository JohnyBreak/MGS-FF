using System.Collections;
using UnityEngine;

public class Detection : MonoBehaviour
{
    [SerializeField] private LookAtState _lookState;
    [SerializeField] private RotationState _rotationState;
    [SerializeField] private LayerMask _layerMask;
    
    private Transform _target;
    private Coroutine _resetRoutine;

    public void Reset()
    {
        _target = null;
        _lookState.Reset();
        _resetRoutine = StartCoroutine(ResetRoutine());
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if ((_layerMask.value & (1 << other.transform.gameObject.layer)) <= 0)
        {
            return;
        }

        _target = other.transform;
        
        StopRoutine();
        _lookState.enabled = true;
        _lookState.SetTarget(_target);
        _rotationState.enabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (_target != other.transform)
        {
            return;
        }

        Reset();
    }

    private IEnumerator ResetRoutine()
    {
        yield return new WaitForSeconds(2);
        _lookState.enabled = false;
        _rotationState.enabled = true;
    }

    private void StopRoutine()
    {
        if (_resetRoutine != null)
        {
            StopCoroutine(_resetRoutine);
        }

        _resetRoutine = null;
    }
}
