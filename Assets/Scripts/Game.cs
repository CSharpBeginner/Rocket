using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Fader _space;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private Player _player;
    [SerializeField] private Altitude _altitude;
    [SerializeField] private Health _health;
    [SerializeField] private Fuel _fuel;
    [SerializeField] private Ground _groundPrefab;
    [SerializeField] private Transition _firstTransition;

    private Transition _transition;
    private Spawner[] _spawners;

    private void Awake()
    {
        Spawner.SetPlayer(_player);
        _spawners = new Spawner[transform.childCount];
        _spawners = GetComponentsInChildren<Spawner>(true);
    }

    private void Start()
    {
        ResetScene();
    }

    private void OnEnable()
    {
        _altitude.Changed += TryChangeTransition;
        _health.Over += Lose;
        _fuel.Over += Lose;
        _space.Finished += Win;
    }

    private void OnDisable()
    {
        _altitude.Changed -= TryChangeTransition;
        _health.Over += Lose;
        _fuel.Over += Lose;
        _space.Finished += Win;
    }

    public static void Exit()
    {
        Application.Quit();
    }

    public void ResetScene()
    {
        _transition = _firstTransition;
        _space.Reset();
        _player.Reset();
        Instantiate(_groundPrefab, _player.transform.position - new Vector3(0, _player.GroundLevel, 0), Quaternion.identity);

        foreach (Spawner spawner in _spawners)
        {
            spawner.DestroyAll();
        }

        Time.timeScale = 0f;
    }

    public void Play()
    {
        _player.enabled = true;
        _player.Launch();

        Time.timeScale = 1f;
    }

    private void TryChangeTransition(float value)
    {
        _transition = _transition?.Transit(value);
    }

    private void Lose()
    {
        foreach (Spawner spawner in _spawners)
        {
            spawner.gameObject.SetActive(false);
        }

        _player.StopMove();
        Time.timeScale = 0f;
        _losePanel.SetActive(true);
    }

    private void Win()
    {
        foreach (Spawner spawner in _spawners)
        {
            spawner.gameObject.SetActive(false);
        }

        _player.StopMove();
        Time.timeScale = 0f;
        _winPanel.SetActive(true);
    }
}
