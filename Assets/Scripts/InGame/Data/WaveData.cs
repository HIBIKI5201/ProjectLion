using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "SpawnData/WaveData")]
public class WaveData : ScriptableObject
{
    [SerializeField, Tooltip("ゲーム開始時からの時間")]
    SpawnData[] _spawnData;

    public SpawnData[] SpawnData
    {
        get => _spawnData;
    }
}

[System.Serializable]
public class SpawnData
{
    [SerializeField] private float _spawnTime;

    [SerializeField] private RandomSpawnData[] _randomSpawnData;

    public float SpawnTime
    {
        get => _spawnTime;
    }

    public RandomSpawnData[] RandomSpawnData
    {
        get => _randomSpawnData;
    }
}

[System.Serializable]
public class RandomSpawnData
{
    [SerializeField] private float _cumulativeWeight = 1;
    [SerializeField] EnemiesSpawnData _spawnData;

    public float CumulativeWeight
    {
        get => _cumulativeWeight;
    }

    public SpawnEnemyData[] Data
    {
        get => _spawnData.EnemyData;
    }
}

[System.Serializable]
public class SpawnEnemyData
{
    [SerializeField] private EnemyGenerator.EnemyKind _kind;
    [SerializeField] private float _statusMultiplier = 1;
    [SerializeField] private float _distance = 15;
    [SerializeField] private int _angle = 0;
    [SerializeField] private int _enemyCount = 1;

    [SerializeField, Tooltip("エネミーをランダムな角度に生成します。この項目がtrueの時Angleは無効化されます。")]
    private bool _randomAngle = false;

    public EnemyGenerator.EnemyKind Kind
    {
        get => _kind;
    }

    public float StatusMultiplier
    {
        get => _statusMultiplier;
    }

    public float Distance
    {
        get => _distance;
    }

    public int Angle
    {
        get => _angle;
    }

    public int EnemyCount
    {
        get => _enemyCount;
    }

    public bool RandomAngle
    {
        get => _randomAngle;
    }
}