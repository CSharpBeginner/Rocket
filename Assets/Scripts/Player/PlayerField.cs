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
        Value = Value > MaxValue - value ? MaxValue : Value + value;
        Changed?.Invoke(Value);
    }

    public void Decrease(float value)
    {
        Value = Value < value ? 0 : Value - value;
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
