using System;
using DG.Tweening;
using UnityEngine;

namespace Collectables
{
    public interface ICollectableView
    {
        void Init(CollectableResolver resolver, ICollectableObject collectable);
        void Collect();
    }

    public class CollectableObjectView : MonoBehaviour, ICollectableView
    {
        [SerializeField] private Collider _collider;
        
        private ICollectableObject _collectable;
        private CollectableResolver _resolver;
        private Vector3 _initialPosition;
        private Sequence _sequence;
            
        private void Start()
        {
            _initialPosition = transform.position;
            transform.DORotate(new Vector3(0, 360, 0), 2, RotateMode.FastBeyond360)
                .SetLoops(-1)
                .SetRelative()
                .SetEase(Ease.Linear);
        }

        public void Init(CollectableResolver resolver, ICollectableObject collectable)
        {
            _resolver = resolver;
            _collectable = collectable;
        }

        public void Collect()
        {
            //if (_resolver.TryCollect(_collectable))
            //{
            //    DOTween.Kill(this);
            //    Destroy(gameObject);                
            //}
            //else
            {
                _collider.enabled = false;
                _sequence?.Kill();
                _sequence = DOTween.Sequence();
                _sequence.Append(transform.DOShakePosition(1, 0.3f, 5, 90f, false, true, ShakeRandomnessMode.Harmonic));
                _sequence.Append(transform.DOMove(_initialPosition, 0.1f));
                _sequence.OnComplete(() => _collider.enabled = true);
            }
        }
    }
}

