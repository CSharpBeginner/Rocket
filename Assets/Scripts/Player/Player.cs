using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Speed))]
[RequireComponent(typeof(Rotation))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Altitude))]
[RequireComponent(typeof(Fuel))]
[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _groundLevel;

    private Altitude _altitude;
    private Fuel _fuel;
    private Health _health;
    private Speed _speed;
    private Rigidbody2D _rigidbody2D;
    private Rotation _rotation;
    private Animator _animator;

    public event UnityAction<Vector2> VelocityChanged;

    public float GroundLevel => _groundLevel;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _speed = GetComponent<Speed>();
        _rotation = GetComponent<Rotation>();
        _animator = GetComponent<Animator>();
        _altitude = GetComponent<Altitude>();
        _health = GetComponent<Health>();
        _fuel = GetComponent<Fuel>();
        transform.rotation = _rotation.DefaultQuaternion;
    }

    private void Update()
    {
        Quaternion target;

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 targetDirection = new Vector3(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y, 0).normalized;
            target = _rotation.GetAvailableQuaternion(targetDirection);
        }
        else
        {
            target = _rotation.DefaultQuaternion;
        }

        transform.rotation = _rotation.RotateTowards(transform.rotation, target);
        float angle = _rotation.GetAngle(transform.rotation);
        _rigidbody2D.velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * _speed.Value;
        VelocityChanged?.Invoke(_rigidbody2D.velocity);
        _altitude.UpdateValue(transform.position.y);
    }

    public void StopMove()
    {
        _speed.ResetSpeed();
        _rigidbody2D.velocity = Vector2.zero;
        _fuel.StopConsumption();
        enabled = false;
    }

    public void Reset()
    {
        transform.SetPositionAndRotation(Vector3.zero, _rotation.DefaultQuaternion);
        _altitude.SetStartPositionY(transform.position.y);
        _altitude.UpdateValue(transform.position.y);
        _health.Reset();
        _fuel.Reset();
    }

    public void Launch()
    {
        _animator.Play(PlayerAnimator.States.Launch);
    }

    private void Fly()
    {
        _speed.Increase(_speed.Normal);
        _fuel.PrepareToConsume();
    }
}
