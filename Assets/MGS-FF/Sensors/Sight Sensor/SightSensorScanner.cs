using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sensors
{
    public class SightSensorScanner
    {
        public event Action OnScanEvent;
        
        private Collider[] _collidersBuffer = new Collider[10];
        private SightSensorMesh _mesh;
        private readonly Transform _transform;
        private readonly LayerMask _targetMask;
        private readonly LayerMask _obstacleMask;
        private readonly Color _color;
        private int _count;
        private float _scanInterval;
        private const int ScanFrequency = 30;
        private float _scanTimer;
        private List<GameObject> _objects = new List<GameObject>();
        private MeshCollider _collider;
        private readonly int _pointsAmount;

        public List<GameObject> Objects
        {
            get
            {
                _objects.RemoveAll(obj => !obj);
                return _objects;
            }
        }

        public SightSensorScanner(
            SightSensorMesh mesh,
            Transform transform,
            LayerMask targetMask,
            LayerMask obstacleMask,
            int pointsAmount,
            Color color)
        {
            _transform = transform;

            _mesh = mesh;
            _collider = _transform.gameObject.AddComponent<MeshCollider>();
            _collider.sharedMesh = _mesh.Mesh;
            _collider.convex = true;

            _targetMask = targetMask;
            _obstacleMask = obstacleMask;
            _pointsAmount = pointsAmount;
            _color = color;
            _scanInterval = 1.0f / ScanFrequency;
        }

        public void Tick()
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
            _count = Physics.OverlapSphereNonAlloc(
                _transform.position,
                _mesh.Distance,
                _collidersBuffer,
                _targetMask,
                QueryTriggerInteraction.Ignore);

            _objects.Clear();
            for (int i = 0; i < _count; i++)
            {
                if (_collidersBuffer[i] == null)
                {
                    continue;
                }

                GameObject obj = _collidersBuffer[i].gameObject;
                if (IsInSight(obj, _collidersBuffer[i]))
                {
                    _objects.Add(obj);
                }
            }
            OnScanEvent?.Invoke();
        }

        private bool IsInSight(GameObject obj, Collider collider)
        {
            bool isOverlapped = Physics.ComputePenetration(_collider, _collider.transform.position,
                _collider.transform.rotation,
                collider, collider.transform.position, collider.transform.rotation, out Vector3 direction,
                out float distance);

            if (!isOverlapped)
            {
                return false;
            }

            DetectComposite composite = obj.GetComponent<DetectComposite>();

            if (composite == null)
            {
                return false;
            }

            if (composite.HasPoints() == false)
            {
                return !LineCast(composite.Position);
            }

            int tempCount = 0;
            foreach (var point in composite.Points)
            {
                if (!LineCast(point.Position))
                {
                    tempCount++;
                }
            }

            return tempCount >= _pointsAmount || tempCount == composite.Points.Length;
        }

        private bool LineCast(Vector3 targetPosition)
        {
            return Physics.Linecast(_transform.position, targetPosition, _obstacleMask);
        }

        public void DrawGizmos()
        {
            if (_mesh.Mesh)
            {
                Gizmos.color = _color;
                Gizmos.DrawMesh(_mesh.Mesh, _transform.position, _transform.rotation);
            }

            Gizmos.DrawWireSphere(_transform.position, _mesh.Distance);

            Gizmos.color = Color.red;
            
            foreach (var col in _collidersBuffer)
            {
                if (col == null)
                {
                    continue;
                }

                Gizmos.DrawSphere(col.transform.position, 1.2f);
            }
            
            
            foreach (var obj in Objects)
            {
                Gizmos.color = Color.green;
                
                Gizmos.DrawSphere(obj.transform.position, 1.2f);
                Gizmos.color = Color.blue;
                
                var composite = obj.GetComponent<DetectComposite>();
                
                if (composite == null)
                {
                    continue;
                }

                if (composite.HasPoints() == false)
                {
                    if (LineCast(composite.Position))
                    {
                        continue;
                    }
                    
                    Gizmos.DrawLine(_transform.position, composite.Position);
                    continue;
                }

                foreach (var point in composite.Points)
                {
                    if (LineCast(point.Position))
                    {
                        continue;
                    }

                    Gizmos.DrawLine(_transform.position, point.Position);
                }
            }
        }
    }
}