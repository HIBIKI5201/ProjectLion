using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "GameData/WeaponData")]
public class WeaponData_S : ScriptableObject
{
    [SerializeField] WeaponData _data;
    public WeaponData Data { get => _data; }
}