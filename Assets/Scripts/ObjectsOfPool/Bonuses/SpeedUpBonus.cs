using UnityEngine;

public class SpeedUpBonus : Bonus
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Speed>(out Speed speed))
        {
            speed.Increase(RestoringValue);
            gameObject.SetActive(false);
        }
    }
}
