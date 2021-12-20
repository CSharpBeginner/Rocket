using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Speed))]
[RequireComponent(typeof(Rotation))]
[RequireComponent(typeof(Account))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Altitude))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _groundLevel;

    private Altitude _altitude;
    private Account _account;
    private Fuel _fuel;
    private Health _health;
    private Speed _speed;
    private Rigidbody2D _rigidbody2D;
    private Rotation _rotation;
    private Animator _animator;
    private bool _isUpdated;

    public event UnityAction<Vector2> VelocityChanged;
    public event UnityAction<float> AltitudeChanged;
    public event UnityAction<float> AccountChanged;
    public event UnityAction Started;
    public event UnityAction Died;

    public float GroundLevel => _groundLevel;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _speed = GetComponent<Speed>();
        _rotation = GetComponent<Rotation>();
        _account = GetComponent<Account>();
        _animator = GetComponent<Animator>();
        _altitude = GetComponent<Altitude>();

        _health = GetComponentInChildren<Health>();
        _fuel = GetComponentInChildren<Fuel>();
        transform.rotation = _rotation.DefaultQuaternion;
        _isUpdated = false;
    }

    private void OnEnable()
    {
        _health.Died += Die;
        _fuel.Died += Die;
        _account.Changed += OnAccountChanged;
        _altitude.Changed += OnAltitudeChanged;
    }

    private void OnDisable()
    {
        _health.Died -= Die;
        _fuel.Died -= Die;
        _account.Changed -= OnAccountChanged;
        _altitude.Changed -= OnAltitudeChanged;
    }

    private void Update()
    {
        if (_isUpdated)
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
    }

    public void StopMove()
    {
        _speed.ResetSpeed();
        _rigidbody2D.velocity = Vector2.zero;
        _fuel.StopConsumption();
        _isUpdated = false;
    }

    public void ResetPlayer()
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
        _isUpdated = true;
        Accelerate(_speed.Normal);
        _fuel.PrepareToConsume();
    }

    private void OnAccountChanged(float value)
    {
        AccountChanged?.Invoke(value);
    }

    private void OnAltitudeChanged(float value)
    {
        AltitudeChanged?.Invoke(value);
    }

    private void Die()
    {
        Died?.Invoke();
        //enabled = false;
    }

    public void IncreaseFuel(int value)
    {
        _fuel.Increase(value);
    }

    public void IncreaseHealth(int value)
    {
        _health.Increase(value);
    }

    public void DecreaseHealth(int value)
    {
        _health.Decrease(value);
    }

    public void IncreaseMoney(int value)
    {
        _account.Increase(value);
    }

    public void DecreaseMoney(int value)
    {
        _account.Decrease(value);
    }

    public void Accelerate(float targetSpeed)
    {
        _speed.Increase(targetSpeed);
    }

    public void IncreaseMaxHealth(float value)
    {
        _health.IncreaseMaxValue(value);
    }

    public void DecreaseMaxHealth(float value)
    {
        _health.DecreaseMaxValue(value);
    }

    public void IncreaseMaxFuel(float value)
    {
        _fuel.IncreaseMaxValue(value);
    }

    public void DecreaseMaxFuel(float value)
    {
        _fuel.DecreaseMaxValue(value);
    }

    public float GetAccountValue()
    {
        return _account.GetValue();
    }
}
