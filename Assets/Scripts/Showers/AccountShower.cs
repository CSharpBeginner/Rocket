using UnityEngine;

public class AccountShower : Shower
{
    [SerializeField] private Account _account;

    private void OnEnable()
    {
        _account.Changed += Show;
    }

    private void OnDisable()
    {
        _account.Changed -= Show;
    }
}
