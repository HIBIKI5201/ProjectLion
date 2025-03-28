using UnityEngine;

[CreateAssetMenu(fileName = "ServantData",menuName = "GameData/ServantData")]
public class ServantData : MobData_S
{
    [SerializeField,Space,Header("サーバント固有設定")] float _moveStopDistanse;
    [SerializeField,Tooltip("使い魔のレベルが上がった時に自動的に上がるステータス倍率 nameステータスは設定しなくても大丈夫です")]
    MobData _levelScaleData;
    public float MoveStopDistanse => _moveStopDistanse;
    public MobData LevelScaleData => _levelScaleData;
}
