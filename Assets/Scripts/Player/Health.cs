using UnityEngine.Events;

public class Health : PlayerField
{
    public event UnityAction Died;

    private void OnEnable()
    {
        Changed += OnChanged;
    }
    private void OnDisable()
    {
        Changed -= OnChanged;
    }

    private void OnChanged(float value)
    {
        if (value <= 0)
        {
            Died?.Invoke();
        }
    }
}
