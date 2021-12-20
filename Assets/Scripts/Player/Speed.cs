using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Speed : MonoBehaviour
{
    [SerializeField] private AnimationCurve _accelerationCurve;
    [SerializeField] private float _animationTimeMultiplier;
    [SerializeField] private float _max;
    [SerializeField] private float _normal;
    [SerializeField] private float _decelerationStep;

    private float _target;
    private Coroutine _currentCoroutine;

    public event UnityAction ExecutedCompletly;

    public float Value { get; private set; }
    public float Normal => _normal;

    private void OnEnable()
    {
        ExecutedCompletly += Decrease;
    }

    private void OnDisable()
    {
        ExecutedCompletly -= Decrease;
    }

    public void ResetSpeed()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        Value = 0;
        _target = 0;
    }

    public void Increase(float value)
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _target += value;

        if (_target >= _max)
        {
            _target = _max;
        }

        _currentCoroutine = StartCoroutine(Accelerate());
    }

    public void Decrease()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentCoroutine = StartCoroutine(Decelerate());
    }

    private IEnumerator Decelerate()
    {
        float deltaTime = 0.05f;
        var waiting = new WaitForSeconds(deltaTime);

        while (Value - _normal > 0.01)
        {
            Value = Mathf.Lerp(Value, _normal, _decelerationStep);
            _target = Value;
            yield return waiting;
        }
    }

    private IEnumerator Accelerate()
    {
        float deltaTime = 0.05f;
        float additionalSpeed = _target - Value;
        float startSpeed = Value;
        float time = _accelerationCurve[0].time;
        float endTime = _accelerationCurve[_accelerationCurve.length - 1].time * _animationTimeMultiplier;
        var waiting = new WaitForSeconds(deltaTime);

        while (time != endTime)
        {
            time = Mathf.MoveTowards(time, endTime, deltaTime);
            Value = startSpeed + additionalSpeed * _accelerationCurve.Evaluate(time / _animationTimeMultiplier);
            yield return waiting;
        }

        if (time == endTime)
        {
            ExecutedCompletly?.Invoke();
        }
    }
}
