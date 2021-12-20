using UnityEngine;

public class FuelBonus : ObjectOfPool
{
    [SerializeField] private int _fuel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.IncreaseFuel(_fuel);
            gameObject.SetActive(false);
        }
    }
}
