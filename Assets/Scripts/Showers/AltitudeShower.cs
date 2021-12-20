using UnityEngine;

public class AltitudeShower : Shower
{
    [SerializeField] private Altitude _altitude;

    private void OnEnable()
    {
        _altitude.Changed += Show;
    }

    private void OnDisable()
    {
        _altitude.Changed -= Show;
    }
}
