using UnityEngine;

[CreateAssetMenu(fileName = "MobData", menuName = "GameData/MobData")]

public class MobData_S : ScriptableObject
{
    [SerializeField] MobData _data;
    public MobData Data { get => _data; }
}

[System.Serializable]
public class MobData
{
    [SerializeField] string _name;
    public string Name { get => _name; }

    [SerializeField]
    private float _maxHealth = 100;
    public float MaxHealth { get => _maxHealth; }

    [SerializeField]
    private float _attack = 20;
    public float Attack { get => _attack; }

    [SerializeField]
    private float _defense = 20;
    public float Defense { get => _defense; }

    [SerializeField]
    private float _agility = 20;
    public float Agility { get => _agility; }

    [SerializeField]
    private float _attackCoolTime = 20;
    public float AttackCoolTime { get => _attackCoolTime; }

    [SerializeField]
    private float _attackRange = 20;
    public float AttackRange { get => _attackRange; }

    public MobData(float health, float attack, float defense, float agility, float attackSpeed, float attackRange)
    {
        _maxHealth = health;
        _attack = attack;
        _defense = defense;
        _agility = agility;
        _attackCoolTime = attackSpeed;
        _attackRange = attackRange;
    }

    public MobData(MobData data, float health, float attack, float defense, float agility, float attackCoolTime, float attackRange)
    {
        _maxHealth = health + data.MaxHealth;
        _attack = attack + data.Attack;
        _defense = defense + data.Defense;
        _agility = agility + data.Agility;
        _attackCoolTime = attackCoolTime + data.AttackCoolTime;
        _attackRange = attackRange + data.AttackRange;
    }
}