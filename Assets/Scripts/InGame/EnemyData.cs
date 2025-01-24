using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "GameData/EnemyData")]
public class EnemyData : MobData_S
{
    [SerializeField]
    private float _dropExperience = 60;
    public float DropExperience { get => _dropExperience; }

    [SerializeField]
    private string _name;
    public string Name { get =>  _name; }
}
