using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxDeflectionAngle;

    private Vector3 _defaultDirection;
    private Vector3 _identityDirection;

    private float _minAvailableAngle => DefaultQuaternion.eulerAngles.z - _maxDeflectionAngle;
    private float _maxAvailableAngle => DefaultQuaternion.eulerAngles.z + _maxDeflectionAngle;
    public Quaternion DefaultQuaternion { get; private set; }

    private void Awake()
    {
        _identityDirection = Quaternion.identity * Vector3.right;
        _defaultDirection = Quaternion.identity * Vector3.up;
        DefaultQuaternion = Quaternion.FromToRotation(_identityDirection, _defaultDirection);
    }

    public Quaternion GetAvailableQuaternion(Vector3 targetDirection)
    {
        Quaternion targetQuaternion = Quaternion.FromToRotation(_identityDirection, targetDirection);

        if (targetQuaternion.eulerAngles.z < _minAvailableAngle || targetQuaternion.eulerAngles.z > DefaultQuaternion.eulerAngles.z + 180)
        {
            return Quaternion.Euler(0, 0, _minAvailableAngle);
        }
        else if (targetQuaternion.eulerAngles.z > _maxAvailableAngle)
        {
            return Quaternion.Euler(0, 0, _maxAvailableAngle);
        }

        return targetQuaternion;
    }

    public Quaternion RotateTowards(Quaternion currentQuaternion, Quaternion targetQuaternion)
    {
        return Quaternion.RotateTowards(currentQuaternion, targetQuaternion, _rotationSpeed * Time.deltaTime);
    }

    public float GetAngle(Quaternion quaternion)
    {
        return quaternion.eulerAngles.z * Mathf.Deg2Rad;
    }
}
