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
        private ICollectableObject _collectable;
        private CollectableResolver _resolver;

        private void Start()
        {
            transform.DORotate(new Vector3(0, 360, 0), 1, RotateMode.FastBeyond360)
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
            _resolver.Collect(_collectable);
            DOTween.Kill(this);
            Destroy(gameObject);
        }
    }
}

