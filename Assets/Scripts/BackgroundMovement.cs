using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _scrollMultiplier;

    private Vector2 _rectPosition;
    private RawImage _rawImage;
    private Rect _target;

    private void Awake()
    {
        _rawImage = GetComponent<RawImage>();
        _rectPosition = _rawImage.uvRect.position;
        _target = _rawImage.uvRect;
    }

    private void OnEnable()
    {
        _player.VelocityChanged += Scroll;
    }

    private void OnDisable()
    {
        _player.VelocityChanged -= Scroll;
    }

    private void Scroll(Vector2 vector2)
    {
        _rectPosition += vector2 * _scrollMultiplier;
        _rectPosition = GetRelativeTarget(_rectPosition);
        _target.Set(_rectPosition.x, _rectPosition.y, _rawImage.uvRect.width, _rawImage.uvRect.height);
        _rawImage.uvRect = _target;
    }

    private Vector2 GetRelativeTarget(Vector2 vector2)
    {
        float positionX = GetRelativeTarget(vector2.x);
        float positionY = GetRelativeTarget(vector2.y);
        return new Vector2(positionX, positionY);
    }

    private float GetRelativeTarget(float value)
    {
        if (value >= 1)
        {
            return value - (int)value;
        }
        else if (value < 0)
        {
            return 1 + ((int)value - value);
        }
        else
        {
            return value;
        }
    }
}
