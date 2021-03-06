using UnityEngine;

public class FuelBonus : Bonus
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Fuel>(out Fuel fuel))
        {
            fuel.Increase(RestoringValue);
            gameObject.SetActive(false);
        }
    }
}
