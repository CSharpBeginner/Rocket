using UnityEngine;

public class Money : Bonuses
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Account>(out Account account))
        {
            account.Increase(RestoringValue);
            gameObject.SetActive(false);
        }
    }
}
