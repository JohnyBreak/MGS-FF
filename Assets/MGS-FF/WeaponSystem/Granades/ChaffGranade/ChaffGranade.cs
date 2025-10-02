using System;
using DamageSystem;
using DG.Tweening;
using UnityEngine;

public class ChaffGranade : MonoBehaviour
{
    private float _time;
    private float _explodeRadius;
    private LayerMask _layerMask;
    
    public void Init(float explodeRadius, float time, LayerMask mask)
    {
        _explodeRadius = explodeRadius;
        _time = time;
        _layerMask = mask;
    }

    public void Activate()
    {
        // start timer
        DOVirtual.Float(0, _time, _time, null)
            .SetEase(Ease.Linear)
            .OnComplete(Explode);
    }

    private void Explode()
    {
        // play fx

        var colliders = Physics.OverlapSphere(transform.position, _explodeRadius, _layerMask);

        foreach (var col in colliders)
        {
            if (col.TryGetComponent<IDamageable>(out var component))
            {
                component.TakeDamage(new ElectricStunDamage(5));
            }
        }
        
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _explodeRadius);
    }
}
