using UnityEngine;

public abstract class Bonuses : ObjectOfPool
{
    [SerializeField] protected float RestoringValue;

    protected abstract void OnTriggerEnter2D(Collider2D collision);
}
