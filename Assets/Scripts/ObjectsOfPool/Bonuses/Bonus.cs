using UnityEngine;

public abstract class Bonus : ObjectOfPool
{
    [SerializeField] protected float RestoringValue;

    protected abstract void OnTriggerEnter2D(Collider2D collision);
}
