using UnityEngine;

public class StartTransition : Transition
{
    [SerializeField] private Spawner[] _spawners;

    protected override void Make()
    {
        foreach (Spawner spawner in _spawners)
        {
            spawner.Initialize();
            spawner.gameObject.SetActive(true);
        }
    }
}
