using UnityEngine;

public class AirplaneTransition : Transition
{
    [SerializeField] private Spawner _airplaneSpawner;
    [SerializeField] private Spawner _birdSpawner;

    protected override void Make()
    {
        _birdSpawner.gameObject.SetActive(false);
        _airplaneSpawner.Initialize();
        _airplaneSpawner.gameObject.SetActive(true);
    }
}
