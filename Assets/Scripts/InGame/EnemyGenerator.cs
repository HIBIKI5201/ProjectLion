using SymphonyFrameWork.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyGenerator : MonoBehaviour
{
    public static ObjectPool<EnemyManager> _enemyPool;
    public Dictionary<GameObject, ObjectPool<EnemyManager>> EnemyPools = new();
    
    [SerializeField]
    private GameObject[] _enemyPrefabs;

    [SerializeField]
    private float _generateIntarval = 1;

    [SerializeField]
    private int _generateLimit = 100;

    private LevelContainer _levelContainer;

    [SerializeField] private bool _test=true;//TODO:HERE テスト用
    public void Initialize()
    {
            ObjectPoolInitialize(_enemyPrefabs);
            StartCoroutine(ObjectGet());
        _levelContainer = FindAnyObjectByType<LevelContainer>();
    }

    #region ObjectPoolパターン
    private void ObjectPoolInitialize(GameObject[] prefab)
    {
        foreach (var obj in prefab)
        {
            if (EnemyPools.ContainsKey(obj)) return;

            var pool = new ObjectPool<EnemyManager>(
                createFunc: () =>Instantiate(obj, transform).GetComponent<EnemyManager>(),
                actionOnGet: enemy => enemy.Init(() => EnemyPools[obj].Release(enemy)),
                actionOnRelease: enemy => { enemy.gameObject.SetActive(false); },
                //AddExperiance(obj.BaseData.DropExperience); },
                actionOnDestroy: enemy => Destroy(enemy.gameObject),
                collectionCheck: false, defaultCapacity: 10, maxSize: _generateLimit);
            EnemyPools[obj] = pool;
        }
    }
    
    private IEnumerator ObjectGet()
    {
        while (true)
        {
            EnemyPools[_enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)]].Get();
            yield return PauseManager.PausableWaitForSecond(_generateIntarval);
        }
    }
    #endregion

    private void AddExperiance(float point)
    {
        _levelContainer.AddExperiance(point);
    }
}
