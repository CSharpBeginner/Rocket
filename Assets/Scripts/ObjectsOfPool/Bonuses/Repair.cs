using UnityEngine;

public class Repair : ObjectOfPool
{
    [SerializeField] private int _health;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.IncreaseHealth(_health);
            gameObject.SetActive(false);
        }
    }
}
