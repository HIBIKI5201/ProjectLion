using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "WeaponUIData", menuName = "WeaponUIData")]
public class WeaponUIDataList : ScriptableObject
{
    [SerializeField] private WeaponUIData[] _detaList;
    public WeaponUIData[] DetaList => _detaList;

    /// <summary>
    /// 武器のUIデータを、武器のステータスデータから検索する
    /// </summary>
    /// <param name="data">検索したい武器データ</param>
    /// <returns>最初に見つかった武器データ。該当がない場合はnull</returns>
    public WeaponUIData GetWeaponUIData(WeaponData data)
    {
        return _detaList.FirstOrDefault(x=>x.WeaponData.Data == data);
    }
}

[System.Serializable]
public class WeaponUIData
{
    [SerializeField] private WeaponData_S _weapondata;
    [SerializeField] private Texture2D _icon;
    [SerializeField] private string _info;
    
    public WeaponData_S WeaponData => _weapondata;
    public Texture2D Icon => _icon;
    public string Info => _info;
}
