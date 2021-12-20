using UnityEngine;
using UnityEngine.Events;

public abstract class PlayerField : MonoBehaviour
{
    [SerializeField] protected float MaxValue;

    protected float Value;

    public virtual event UnityAction<float> Changed;
    public event UnityAction<float> MaxValueChanged;
    public event UnityAction Reseted;

    protected void Awake()
    {
        MaxValueChanged?.Invoke(MaxValue);
        Changed?.Invoke(Value);
    }

    public void Increase(float value)
    {
        float target = Value + value;

        if (target > MaxValue)
        {
            target = MaxValue;
        }

        Value = target;
        Changed?.Invoke(Value);
    }

    public void Decrease(float value)
    {
        float target = Value - value;

        if (target < 0)
        {
            target = 0;
        }

        Value = target;
        Changed?.Invoke(Value);
    }

    public void IncreaseMaxValue(float value)
    {
        MaxValue += value;
        MaxValueChanged?.Invoke(MaxValue);
    }

    public void DecreaseMaxValue(float value)
    {
        MaxValue -= value;
        MaxValueChanged?.Invoke(MaxValue);
    }

    public void Reset()
    {
        Value = MaxValue;
        Reseted?.Invoke();
    }
}
