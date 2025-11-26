using UnityEngine;
using UniRx;

namespace InteractSystem
{
    public class InteractTargetContainer : MonoBehaviour
    {
        public ReactiveProperty<IInteractable> Target;
    }
}