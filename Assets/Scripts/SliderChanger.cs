using UnityEngine;
using UnityEngine.UI;

public class SliderChanger : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Player _player;
    [SerializeField] private int _startPrice;
    [SerializeField] private int _priceMultiplier;
    [SerializeField] private Button _rightButton;
    [SerializeField] private Button _leftButton;

    private bool _hasEnoughMoney;

    private int _currentPrice => _startPrice * (int)Mathf.Pow(_priceMultiplier, (int)_slider.value - 1);
    private int _nextPrice => _startPrice * (int)Mathf.Pow(_priceMultiplier, (int)_slider.value);

    private void OnEnable()
    {
        float account = _player.GetAccountValue();
        OnSliderChanged(_slider.value);
        OnAccountChanged(account);
        _slider.onValueChanged.AddListener(OnSliderChanged);
        _player.AccountChanged += OnAccountChanged;
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(OnSliderChanged);
        _player.AccountChanged -= OnAccountChanged;
    }

    public void IncreaseAccount()
    {
        _player.IncreaseMoney(_nextPrice);
    }

    public void DecreaseAccount()
    {
        _player.DecreaseMoney(_currentPrice);
    }

    public void Increase()
    {
        _slider.value++;
    }

    public void Decrease()
    {
        _slider.value--;
    }

    private void OnAccountChanged(float value)
    {
        _hasEnoughMoney = value >= _nextPrice;
        TryChangeRightButtonsInteractivity();
    }

    private bool CheckInteractivityConditionsForRightButton()
    {
        return _hasEnoughMoney && _slider.value < _slider.maxValue;
    }

    private bool CheckInteractivityConditionsForLeftButton()
    {
        return _slider.value > _slider.minValue;
    }

    private void OnSliderChanged(float value)
    {
        TryChangeLeftButtonsInteractivity();
    }

    private void TryChangeLeftButtonsInteractivity()
    {
        _leftButton.interactable = CheckInteractivityConditionsForLeftButton();
    }

    private void TryChangeRightButtonsInteractivity()
    {
        _rightButton.interactable = CheckInteractivityConditionsForRightButton();
    }
}
