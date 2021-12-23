using UnityEngine;

public class Repair : Bonuses
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Health>(out Health health))
        {
            health.Increase(RestoringValue);
            gameObject.SetActive(false);
        }
    }
}
