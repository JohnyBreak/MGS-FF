using UnityEngine;

namespace Sensors
{
    public class DetectComposite : MonoBehaviour
    {
        private DetectPoint[] _points;
        public DetectPoint[] Points => _points;
        public Vector3 Position => transform.position;
        
        private void Start()
        {
            _points = GetComponentsInChildren<DetectPoint>();
        }

        public bool HasPoints()
        {
            return Points != null && Points.Length > 0;
        }
    }
}