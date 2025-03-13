using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesSpawnData", menuName = "SpawnData/EnemiesSpawnData")]
public class EnemiesSpawnData : ScriptableObject
{
    [SerializeField] private SpawnEnemyData[] _enemyData;
    public SpawnEnemyData[] EnemyData
    {
        get => _enemyData;
    }
}