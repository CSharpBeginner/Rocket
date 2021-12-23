using UnityEngine.Events;

public class Health : PlayerField
{
    public event UnityAction Over;

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
            Over?.Invoke();
        }
    }
}
