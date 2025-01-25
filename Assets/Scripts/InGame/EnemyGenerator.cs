using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyGenerator : MonoBehaviour
{
    public static ObjectPool<EnemyManager> _enemyPool;
    [SerializeField]
    private GameObject[] _enemyPrefabs;

    [SerializeField]
    private float _generateIntarval = 1;

    [SerializeField]
    private int _generateLimit = 100;

    private LevelContainer _levelContainer;


    public void Initialize()
    {
        ObjectPoolInitialize();
        StartCoroutine(ObjectGet());
        _levelContainer = FindAnyObjectByType<LevelContainer>();
    }

    #region ObjectPoolパターン
    private void ObjectPoolInitialize()
    {
        _enemyPool = new ObjectPool<EnemyManager>(
            createFunc: () =>
                {
                    var enemy = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)];
                    return Instantiate(enemy, transform).GetComponent<EnemyManager>();
                },
            actionOnGet: obj => obj.Init(() => _enemyPool.Release(obj)),
            actionOnRelease: obj => { obj.gameObject.SetActive(false); AddExperiance(obj.BaseData.DropExperience); },
            actionOnDestroy: obj => Destroy(obj.gameObject),
            collectionCheck: false, defaultCapacity: 10, maxSize: _generateLimit);
    }

    private IEnumerator ObjectGet()
    {
        while (true)
        {
            if (_enemyPool.CountActive <= _generateLimit)
            {
                _enemyPool.Get();
            }
            yield return GameLogics.PausalbeWaitForSecond(_generateIntarval);
        }
    }
    #endregion

    private void AddExperiance(float point)
    {
        _levelContainer.AddExperiance(point);
    }
}
