using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class RocketUpgrading : MonoBehaviour
{
    [SerializeField] private Account _account;
    [SerializeField] private int _startPrice;
    [SerializeField] private int _priceMultiplier;
    [SerializeField] private Button _upgradingButton;
    [SerializeField] private Button _degradingButton;
    [SerializeField] private float _maxLevel;

    private float _level;

    private int _currentPrice => _startPrice * (int)Mathf.Pow(_priceMultiplier, (int)_level - 1);
    private int _nextPrice => _startPrice * (int)Mathf.Pow(_priceMultiplier, (int)_level);

    public float MaxLevel => _maxLevel;

    public event UnityAction<float> Changed;

    private void Awake()
    {
        _level = 0;
    }

    private void OnEnable()
    {
        Changed += TryChangeInteractivityOfDegradingButton;
        Changed?.Invoke(_level);
        float account = _account.GetValue();
        IsUpgradingAvailable(account);
        _account.Changed += TryChangeInteractivityOfUpgradingButton;
    }

    private void OnDisable()
    {
        Changed -= TryChangeInteractivityOfDegradingButton;
        _account.Changed -= TryChangeInteractivityOfUpgradingButton;
    }

    public void IncreaseAccount()
    {
        _account.Increase(_nextPrice);
    }

    public void DecreaseAccount()
    {
        _account.Decrease(_currentPrice);
    }

    public void Upgrade()
    {
        _level++;
        Changed?.Invoke(_level);
    }

    public void Degrade()
    {
        _level--;
        Changed?.Invoke(_level);
    }

    private void TryChangeInteractivityOfUpgradingButton(float account)
    {
        _upgradingButton.interactable = IsUpgradingAvailable(account);
    }

    private void TryChangeInteractivityOfDegradingButton(float level)
    {
        _degradingButton.interactable = IsDegradingAvailable(level);
    }

    private bool IsUpgradingAvailable(float account)
    {
        return account >= _nextPrice && _level < _maxLevel;
    }

    private bool IsDegradingAvailable(float level)
    {
        return level > 0;
    }
}
