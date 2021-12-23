using UnityEngine;

public class HealthFiller : Filler
{
    [SerializeField] private Health _health;

    protected override void Awake()
    {
        base.Awake();
        TargetField = _health;
    }
}
