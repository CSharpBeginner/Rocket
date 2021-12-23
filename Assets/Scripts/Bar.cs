using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class Bar : MonoBehaviour
{
    [SerializeField] private RocketUpgrading _rocketUpgrading;

    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _rocketUpgrading.MaxLevel;
    }

    private void OnEnable()
    {
        _rocketUpgrading.Changed += SetValue;
    }

    private void OnDisable()
    {
        _rocketUpgrading.Changed -= SetValue;
    }

    private void SetValue(float value)
    {
        _slider.value = value;
    }
}
