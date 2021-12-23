using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Obstacle : ObjectOfPool
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;
    private Vector2 _direction;
    private Vector2 _velocity;
    private Coroutine _currentCoroutine;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        Reset();
        ChooseDirection();
        _velocity = _direction * _speed;
        _currentCoroutine = StartCoroutine(ChangeVelocity());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Health>(out Health health))
        {
            health.Decrease(_damage);

            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
                _rigidbody2D.AddForce(collision.relativeVelocity, ForceMode2D.Impulse);
                _rigidbody2D.gravityScale = 1f;
            }
        }
        else if (collision.gameObject.TryGetComponent<Obstacle>(out Obstacle obstacle))
        {
            Physics2D.IgnoreCollision(obstacle.GetComponent<Collider2D>(), _collider2D);
        }
    }

    private void Reset()
    {
        _rigidbody2D.angularVelocity = 0;
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.gravityScale = 0f;
        transform.rotation = Quaternion.identity;
    }

    private IEnumerator ChangeVelocity()
    {
        var waiting = new WaitForSeconds(0.05f);

        while (true)
        {
            _rigidbody2D.velocity = _velocity;
            yield return waiting;
        }
    }

    private void ChooseDirection()
    {
        if (Player.transform.position.x >= transform.position.x)
        {
            _spriteRenderer.flipX = false;
            _direction = Vector2.right;
        }
        else
        {
            _spriteRenderer.flipX = true;
            _direction = Vector2.left;
        }
    }
}
