using UnityEngine;

public class FuelFiller : Filler
{
    [SerializeField] private Fuel _fuel;

    protected override void Awake()
    {
        base.Awake();
        TargetField = _fuel;
    }
}
