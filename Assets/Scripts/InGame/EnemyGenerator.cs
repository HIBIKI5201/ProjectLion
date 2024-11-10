using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyGenerator : MonoBehaviour
{
    public static ObjectPool<EnemyManager> _enemyPool;
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private float _generateIntarval = 1;

    [SerializeField]
    private int _generateLimit = 100;


    public void Initialize()
    {
        ObjectPoolInitialize();
        StartCoroutine(ObjectGet());
    }

    #region ObjectPoolパターン
    private void ObjectPoolInitialize()
    {
        _enemyPool = new ObjectPool<EnemyManager>(
            createFunc: () => Instantiate(_enemyPrefab, transform).GetComponent<EnemyManager>(),
            actionOnGet: obj => obj.Init(() => _enemyPool.Release(obj)),
            actionOnRelease: obj => obj.gameObject.SetActive(false),
            actionOnDestroy: obj => Destroy(obj.gameObject),
            collectionCheck: true,
            defaultCapacity: 10,
            maxSize: _generateLimit);
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
}
