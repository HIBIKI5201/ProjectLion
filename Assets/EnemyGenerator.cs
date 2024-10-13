using UnityEngine;
using UnityEngine.Pool;

public class EnemyGenerator : MonoBehaviour
{
    public static ObjectPool<EnemyManager> _enemyPool;
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private float _generateIntarval = 1;
    private float _generateTimer = 0;
    void Start()
    {
        _enemyPool = new ObjectPool<EnemyManager>(
        createFunc: () =>
            {
                Debug.Log("Instantiate");
                return Instantiate(_enemyPrefab, transform).GetComponent<EnemyManager>();
            },
        actionOnGet: obj => obj.Init(() => _enemyPool.Release(obj)),
        actionOnRelease: obj => Debug.Log($"{obj.gameObject.name}��Release"),
        actionOnDestroy: obj => Destroy(obj.gameObject),
        collectionCheck: true,
        defaultCapacity: 10,
        maxSize: 100);
    }

    private void Update()
    {
        _generateTimer += Time.deltaTime;
        Debug.Log(transform.childCount);
        if (_generateTimer > _generateIntarval && _enemyPool.CountActive <= 100)
        {
            _generateTimer = 0;
            _enemyPool.Get();
        }
    }
}
