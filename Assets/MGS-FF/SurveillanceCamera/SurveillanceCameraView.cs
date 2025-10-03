using UnityEngine;

namespace SurveillanceCameraSystem
{
    public class SurveillanceCameraView : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Collider _collider;
        [SerializeField] private Transform _rotator;
    
        public LayerMask LayerMask => _layerMask;
        public Collider Collider => _collider;
        public Transform Rotator => _rotator;
    }
}

