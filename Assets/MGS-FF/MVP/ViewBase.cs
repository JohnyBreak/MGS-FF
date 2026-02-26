using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MVP
{
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(GraphicRaycaster))]
    public class ViewBase : MonoBehaviour, IView
    {
        protected Canvas Canvas { get; private set; }
        private GraphicRaycaster Raycaster { get; set; }

        [UsedImplicitly]
        protected virtual void Awake()
        {
            Canvas = GetComponent<Canvas>();
            Raycaster = GetComponent<GraphicRaycaster>();
        }
        
        public void Initialize()
        {
            OnInitialize();
        }
        
        public void Dispose()
        {
            OnDispose();
            Destroy(gameObject);
        }

        public virtual void SetActive(bool value)
        {
            Canvas.enabled = value;
        }

        public virtual void SetInputActive(bool value)
        {
            Raycaster.enabled = value;
        }

        protected virtual void OnInitialize() { }

        protected virtual void OnDispose() { }
    }
}