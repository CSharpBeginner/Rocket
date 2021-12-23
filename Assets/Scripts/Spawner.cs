using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private ObjectOfPool _prefab;
    [SerializeField] private SquareOfSpawn _square;
    [SerializeField] private SquareOfSpawn _initilizeSquare;
    [SerializeField] private int _poolLength;

    private static Player _player;

    private ObjectOfPool[] _pool;

    private void OnEnable()
    {
        foreach (ObjectOfPool poolObject in _pool)
        {
            poolObject.Disabled += Respawn;
        }
    }

    private void OnDisable()
    {
        foreach (ObjectOfPool poolObject in _pool)
        {
            poolObject.Disabled -= Respawn;
        }
    }

    public static void SetPlayer(Player player)
    {
        _player = player;
    }

    public void DestroyAll()
    {
        if (_pool != null)
        {
            foreach (ObjectOfPool poolObject in _pool)
            {
                if (poolObject != null)
                {
                    Destroy(poolObject.gameObject);
                }
            }
        }
    }

    public void Initialize()
    {
        _pool = new ObjectOfPool[_poolLength];
        ObjectOfPool.SetPlayer(_player);

        for (int i = 0; i < _poolLength; i++)
        {
            ObjectOfPool spawnedGameObject = Instantiate(_prefab, _container);
            Spawn(spawnedGameObject, _initilizeSquare);
            _pool[i] = spawnedGameObject;
        }
    }

    private void Spawn(ObjectOfPool spawnedObject, SquareOfSpawn spawnSquare)
    {
        spawnedObject.transform.position = new Vector2(Random.Range(spawnSquare.LeftBottomCorner.x, spawnSquare.LeftBottomCorner.x + spawnSquare.Width), Random.Range(_square.LeftBottomCorner.y, spawnSquare.LeftBottomCorner.y + spawnSquare.Height));
        spawnedObject.gameObject.SetActive(true);
    }

    private void Spawn(ObjectOfPool spawnedObject)
    {
        Spawn(spawnedObject, _square);
    }

    private void Respawn(ObjectOfPool spawnedObject)
    {
        StartCoroutine(SpawnInNextFrame(spawnedObject));
    }

    private IEnumerator SpawnInNextFrame(ObjectOfPool spawnedObject)
    {
        yield return null;
        Spawn(spawnedObject);
    }
}
