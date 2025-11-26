using System;
using System.Collections.Generic;
using UnityEngine;

namespace InteractSystem
{
    // редко обновлять. проверять какой объект ближе к transform.forward,
    // класть его в контейнер,
    // при смене объекта в контейнере оповещать персонажа, что он может взаимодействовать
    // при оповещении можно включить ui мол нажми Е чтобы говорить или открыть,
    // мб сделать как в rental знак вопроса над головой персонажа или объекта

    public class InteractScanner : MonoBehaviour 
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _radius;
        [SerializeField] private bool _drawDebug;
        [SerializeField] private InteractTargetContainer _container;
        
        private Collider[] _collidersBuffer = new Collider[5];
        private float _scanInterval;
        private const int ScanFrequency = 10;
        private float _scanTimer;

        private void Start()
        {
            _scanInterval = 1.0f / ScanFrequency;
        }

        public void Update()
        {
            _scanTimer -= Time.deltaTime;
            if (_scanTimer < 0)
            {
                _scanTimer += _scanInterval;
                Scan();
            }
        }

        private void Scan()
        {
            int count = Physics.OverlapSphereNonAlloc(
                transform.position,
                _radius,
                _collidersBuffer,
                _layerMask,
                QueryTriggerInteraction.Ignore);

            if (count < 1)
            {
                _container.Target.Value = null;
                return;
            }

            List<Transform> interactables = new List<Transform>(_collidersBuffer.Length);
            
            foreach (var col in _collidersBuffer)
            {
                if (!col)
                {
                    continue;
                }

                if (col.TryGetComponent<IInteractable>(out var interactable))
                {
                    interactables.Add(col.transform);
                }
            }

            Transform best = null;
            float bestDot = -1f;

            foreach (var col in interactables)
            {
                Vector3 dir = (col.transform.position - transform.position).normalized;

                float dot = Vector3.Dot(transform.forward, dir);

                if (dot > bestDot)
                {
                    bestDot = dot;
                    best = col.transform;
                }
            }
            
            _container.Target.Value = (best != null)? best.GetComponent<IInteractable>() : null;
        }

        private void OnDrawGizmos()
        {
            if (!_drawDebug)
            {
                return;
            }

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}