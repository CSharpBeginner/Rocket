using UnityEngine;

public class Repair : Bonus
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
