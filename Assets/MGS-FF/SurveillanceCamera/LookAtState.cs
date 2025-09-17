using UnityEngine;

public class LookAtState : MonoBehaviour
{
    [SerializeField] private Detection _detection;
    [SerializeField] private float _speed = 3f;
    
    [SerializeField] private float _maxPitch = 55f;
    [SerializeField] private float _minPitch = 0f;
    [SerializeField] private float _minYaw = -65f;
    [SerializeField] private float _maxYaw = 65f;
    
    private Transform _target;
    private bool _canLook;

    public void SetTarget(Transform target)
    {
        _target = target;
        _canLook = true;
    }
    
    public void Reset()
    {
        _target = null;
        _canLook = false;
    }
    
    private void Update()
    {
        if (!_target) return;

        Vector3 localTargetDir = transform.parent ? 
            transform.parent.InverseTransformDirection(_target.position - transform.position) : 
            _target.position - transform.position;

        if (localTargetDir == Vector3.zero)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(localTargetDir);

        Vector3 targetEuler = targetRotation.eulerAngles;

        targetEuler = NormalizeAngles(targetEuler);

        float deltaYaw = Mathf.Clamp(targetEuler.y, _minYaw, _maxYaw);
        float deltaPitch = Mathf.Clamp(targetEuler.x, _minPitch, _maxPitch);

        Vector3 clampedEuler = new Vector3(
            deltaPitch,
            deltaYaw,
            0f
        );

        Quaternion clampedRotation = Quaternion.Euler(clampedEuler);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, clampedRotation, _speed * Time.deltaTime);
    }

    private Vector3 NormalizeAngles(Vector3 angles)
    {
        return new Vector3(
            NormalizeAngle(angles.x),
            NormalizeAngle(angles.y),
            NormalizeAngle(angles.z)
        );
    }

    private float NormalizeAngle(float angle)
    {
        while (angle > 180f) angle -= 360f;
        while (angle < -180f) angle += 360f;
        return angle;
    }
}
