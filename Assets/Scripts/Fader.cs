using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class Fader : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _multiplier;

    private float _startAltitude;
    private RawImage _rawImage;

    public event UnityAction Finished;

    private void OnEnable()
    {
        _player.AltitudeChanged += CalculateValue;
    }

    private void OnDisable()
    {
        _player.AltitudeChanged -= CalculateValue;
    }

    public void SetStartAltitude(float value)
    {
        _startAltitude = value;
    }

    public void Reset()
    {
        _rawImage = GetComponent<RawImage>();
        _rawImage.color = new Color(_rawImage.color.r, _rawImage.color.g, _rawImage.color.b, 0);
        gameObject.SetActive(false);
    }

    private void CalculateValue(float altitude)
    {
        float transparency;
        transparency = (altitude - _startAltitude) * _multiplier;

        if (transparency < 1)
        {
            _rawImage.color = new Color(_rawImage.color.r, _rawImage.color.g, _rawImage.color.b, transparency);
        }
        else
        {
            _rawImage.color = new Color(_rawImage.color.r, _rawImage.color.g, _rawImage.color.b, 1);
            Finished?.Invoke();
            enabled = false;
        }
    }
}
