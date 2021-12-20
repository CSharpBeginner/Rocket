using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Filler : MonoBehaviour
{
    [SerializeField] private float _step;
    [SerializeField] private PlayerField _targetGameObject;

    private Coroutine _currentCoroutine;
    private Image _image;
    private float _maxValue;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        _targetGameObject.Reseted += Reset;
        _targetGameObject.MaxValueChanged += OnMaxValueChanged;
        _targetGameObject.Changed += OnChanged;
    }

    private void OnDisable()
    {
        _targetGameObject.Reseted -= Reset;
        _targetGameObject.MaxValueChanged -= OnMaxValueChanged;
        _targetGameObject.Changed -= OnChanged;
    }

    private void Reset()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _image.fillAmount = 1;
    }

    private void OnChanged(float value)
    {
        float target = value / _maxValue;

        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentCoroutine = StartCoroutine(ChangeValue(target));
    }

    private IEnumerator ChangeValue(float target)
    {
        var waiting = new WaitForSeconds(0.05f);

        while (_image.fillAmount != target)
        {
            _image.fillAmount = Mathf.MoveTowards(_image.fillAmount, target, _step);
            yield return waiting;
        }
    }

    private void OnMaxValueChanged(float value)
    {
        _maxValue = value;
    }
}
