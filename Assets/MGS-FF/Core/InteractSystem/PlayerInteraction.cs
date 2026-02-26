using UniRx;
using UnityEngine;

namespace InteractSystem
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private InteractTargetContainer _container;

        private void Start()
        {
            _container?.Target
                .DistinctUntilChanged()
                .Subscribe(OnTarget).AddTo(this);
        }

        private void OnTarget(IInteractable interactable)
        {
            //Debug.Log(interactable != null ? $"can interact" : $"no interact target");
        }

        private void Update()
        {
            if (GameState.GameState.CurrentState != GameState.GameState.State.GamePlay)
            {
                return;
            }
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_container.Target.HasValue == false)
                {
                    return;
                }
                if (_container.Target.Value == null)
                {
                    return;
                }
                _container.Target.Value.Interact();
            }
        }
    }
}