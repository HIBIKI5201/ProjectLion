using SymphonyFrameWork.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public class EnemyGenerator : MonoBehaviour
{
    public static ObjectPool<EnemyManager> _enemyPool;
    public Dictionary<EnemyKind, ObjectPool<EnemyManager>> EnemyPools = new();

    [SerializeField] private EnemyData[] _enemyKinds;
    [SerializeField] private MasterSpawnData _spawnData;

    [SerializeField] private float _generateIntarval = 1;

    [SerializeField] private int _generateLimit = 100;

    private LevelContainer _levelContainer;

    [SerializeField] private bool _test = true; //TODO:HERE テスト用

    public void Initialize()
    {
        ObjectPoolInitialize(_enemyKinds);
        StartCoroutine(ObjectGet());
        _levelContainer = FindAnyObjectByType<LevelContainer>();
    }

    #region ObjectPoolパターン

    private void ObjectPoolInitialize(EnemyData[] data)
    {
        foreach (var obj in data)
        {
            if (EnemyPools.ContainsKey(obj.Kind)) return;

            var pool = new ObjectPool<EnemyManager>(
                createFunc: () => Instantiate(obj.EnemyPrefab, transform).GetComponent<EnemyManager>(),
                //actionOnGet: enemy => enemy.Init(() => EnemyPools[obj.Kind].Release(enemy),1),
                actionOnRelease: enemy => { enemy.gameObject.SetActive(false); },
                //AddExperiance(obj.BaseData.DropExperience); },
                actionOnDestroy: enemy => Destroy(enemy.gameObject),
                collectionCheck: false, defaultCapacity: 10, maxSize: _generateLimit);
            EnemyPools[obj.Kind] = pool;
        }
    }

    private IEnumerator ObjectGet()
    {
        while (_test)
        {
            var n = _enemyKinds[Random.Range(0, _enemyKinds.Length)].Kind;
            EnemyManager enemy　= EnemyPools[n].Get();
            enemy.Init(() => EnemyPools[n].Release(enemy), 1);
            enemy.SetPos(0, 10);

            yield return PauseManager.PausableWaitForSecond(_generateIntarval);
        }

        int loopCount = 0;
        float lastSpawnTime = 0;
        while (!_test&&loopCount < _spawnData.SpawnData.Length)
        {
            yield return PauseManager.PausableWaitForSecond(_spawnData.SpawnData[loopCount].SpawnTime - lastSpawnTime);
         
            var data =RandomSpawn();
            if (data is null)
            {
                Debug.LogWarning("SpawnData is null ");
                continue;
            }
            //エネミー生成
            foreach (var enemyData in data) //4重ループぐらいになってて重そうだな～。
            {
                for (int i = 0; i < enemyData.EnemyCount; i++)
                {
                    EnemyManager enemy　= EnemyPools[enemyData.Kind].Get();
                    enemy.Init(() => EnemyPools[enemyData.Kind].Release(enemy), enemyData.StatusMultiplier);
                    enemy.SetPos(enemyData.Angle, enemyData.Distance);
                }
            }
            
            lastSpawnTime = _spawnData.SpawnData[loopCount].SpawnTime;
            loopCount++;

        }
        SpawnEnemyData[] RandomSpawn()
        {
            //加重による格率決定
            var totalWeight =_spawnData.SpawnData[loopCount].RandomSpawnData.Sum(x => x.CumulativeWeight);
            float sum = 0;
            float randomWeight = Random.Range(0, totalWeight);
            Debug.Log($"{totalWeight}, {randomWeight}");
            foreach (var obj in _spawnData.SpawnData[loopCount].RandomSpawnData)
            {
                sum += obj.CumulativeWeight;
                if (sum > randomWeight)
                {
                    return obj.Data;
                }
            }
                
            Debug.Log("ランダムが最後まで生成されました！");
            return null;
        }
    }

    #endregion

    private void AddExperiance(float point)
    {
        _levelContainer.AddExperiance(point);
    }

    [System.Serializable]
    public struct EnemyData
    {
        [SerializeField] private EnemyManager _enemyPrefab;
        [SerializeField] private EnemyKind _enemyKind;

        public EnemyManager EnemyPrefab
        {
            get => _enemyPrefab;
        }

        public EnemyKind Kind
        {
            get => _enemyKind;
        }
    }

    public enum EnemyKind
    {
        Speed,
        Defense,
        Explode,
        Attack,
        Mini,
    }
}