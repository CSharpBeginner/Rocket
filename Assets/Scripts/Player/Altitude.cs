using UnityEngine;
using UnityEngine.Events;

public class Altitude : MonoBehaviour
{
    [SerializeField] private float _multiplier;

    private float _value;
    private float _startPositionY;

    public event UnityAction<float> Changed;

    public void SetStartPositionY(float startPositionY)
    {
        _startPositionY = startPositionY;
    }

    public void UpdateValue(float positionY)
    {
        _value = (positionY - _startPositionY) * _multiplier;
        Changed?.Invoke(_value);
    }
}
