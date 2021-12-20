using UnityEngine;

public class SpeedUpBonus : ObjectOfPool
{
    [SerializeField] private float _speed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.Accelerate(_speed);
            gameObject.SetActive(false);
        }
    }
}
