using UnityEngine;

[CreateAssetMenu(fileName = "MobData", menuName = "GameData/MobData")]
public class MobData : ScriptableObject
{
    [SerializeField]
    private float _maxHealth = 100;
    public float MaxHealth { get => _maxHealth; }

    [SerializeField]
    private float _attack = 10;
    public float Attack { get => _attack; }

    [SerializeField]
    private float _defense = 10;
    public float Defense { get => _defense; }

    [SerializeField]
    private float _agility = 10;
    public float Agility { get => _agility; }

    [SerializeField]
    private float _attackSpeed = 10;
    public float AttackSpeed { get => _attackSpeed; }

    [SerializeField]
    private float _attackRange = 10;
    public float AttackRange { get => _attackRange; }
}
