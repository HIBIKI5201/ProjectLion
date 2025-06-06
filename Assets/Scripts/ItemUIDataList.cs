using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MastorData/ItemDataMastor", fileName = "ItemDataMastor")]
public class ItemUIDataList : ScriptableObject
{
    [SerializeField]List<ItemUIData> itemData;
    public ItemUIData FindEnum(ItemKind kind) => itemData.Find(x => x.Kind == kind);
}
public class ItemUIDataList<T> : ScriptableObject where T : DataBase
{
    protected List<T> _datas = new();
    public T FindID(int id) => _datas.Find(x => x.ID == id);
    public T FindName(string name) => _datas.Find(x => x.Name == name);
}
public class DataBase
{
    [SerializeField] int id;
    public int ID => id;
    [SerializeField] string name;
    public string Name => name;
}