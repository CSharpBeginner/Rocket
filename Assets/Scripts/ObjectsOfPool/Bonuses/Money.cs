using UnityEngine;

public class Money : ObjectOfPool
{
    [SerializeField] private int _money;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.IncreaseMoney(_money);
            gameObject.SetActive(false);
        }
    }
}
