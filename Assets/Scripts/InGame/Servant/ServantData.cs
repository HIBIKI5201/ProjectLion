using UnityEngine;

[CreateAssetMenu(fileName = "ServantData",menuName = "GameData/ServantData")]
public class ServantData : MobData_S
{
    [SerializeField] string _name;
    public string Name { get { return _name; } }
}
