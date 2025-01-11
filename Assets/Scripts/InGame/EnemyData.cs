using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "GameData/EnemyData")]
public class EnemyData : MobData
{
    [SerializeField]
    private float _dropExperience = 60;
    public float DropExperience { get => _dropExperience; }

    [SerializeField]
    private string _name;
    public string Name { get =>  _name; }

    public EnemyData(float health, float attack, float defense, float agility, float attackSpeed, float attackRange) : base(health, attack, defense, agility, attackSpeed, attackRange)
    {
    }
}
