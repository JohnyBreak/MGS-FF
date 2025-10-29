using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Collectables
{
    public interface ICollectableView
    {
        void Init(CollectableResolver resolver, ICollectableObject collectable, CollectableFloatingTextCanvas canvas);
        void Collect();
    }

    public class CollectableObjectView : MonoBehaviour, ICollectableView
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private Collider _textCollider;
        [SerializeField] private LayerMask _mask;

        private CollectableFloatingTextCanvas _canvas;
        private ICollectableObject _collectable;
        private CollectableResolver _resolver;
        private Vector3 _initialPosition;
        private Sequence _sequence;
        private CompositeDisposable _disposable = new CompositeDisposable();
        private int _hashCode = -1;

        public int HashCode {
            get
            {
                if (_hashCode < 0)
                {
                    _hashCode = this.GetHashCode();
                }

                return _hashCode;
            }
        }

        private void Start()
        {
            _textCollider
                .OnTriggerEnterAsObservable()
                .Subscribe(OnTextTriggerEnter)
                .AddTo(_disposable);
            _textCollider
                .OnTriggerExitAsObservable()
                .Subscribe(OnTextTriggerExit)
                .AddTo(_disposable);
            
            _initialPosition = transform.position;
            transform.DORotate(new Vector3(0, 360, 0), 2, RotateMode.FastBeyond360)
                .SetLoops(-1)
                .SetRelative()
                .SetEase(Ease.Linear);
        }

        private void OnTextTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & _mask) == 0)
            {
                return;
            }

            if (_canvas == null)
            {
                return;
            }
            _canvas.Show(this);
        }
        
        private void OnTextTriggerExit(Collider other)
        {
            if (((1 << other.gameObject.layer) & _mask) == 0)
            {
                return;
            }

            if (_canvas == null)
            {
                return;
            }
            _canvas.Hide(this);
        }
        
        public void Init(
            CollectableResolver resolver, 
            ICollectableObject collectable,
            CollectableFloatingTextCanvas canvas)
        {
            _resolver = resolver;
            _collectable = collectable;
            _canvas = canvas;
        }

        public void Collect()
        {
            if (_resolver.TryCollect(_collectable))
            {
                _canvas.Hide(this);
                DOTween.Kill(this);
                Destroy(gameObject);                
            }
            else
            {
                if (_canvas != null)
                {
                    _canvas.SetRedText(this);
                }

                _collider.enabled = false;
                _sequence?.Kill();
                _sequence = DOTween.Sequence();
                _sequence.Append(transform.DOShakePosition(1, 0.3f, 5, 90f, false, true, ShakeRandomnessMode.Harmonic));
                _sequence.Append(transform.DOMove(_initialPosition, 0.1f));
                _sequence.OnComplete(() => _collider.enabled = true);
            }
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }
    }
}

