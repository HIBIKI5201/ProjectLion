using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "BuffServantData", menuName = "ServantData/BuffServantData")]
public class BuffServantData_S : ServantData
{
    [Space] [SerializeField, Tooltip("スキル継続時間")] float _skillTime;
    [SerializeField] float _skillCoolTime;
    [Space] [SerializeField, Tooltip("バフの種類(1種類のみ)")] BuffKind _buffKind;
    [SerializeField, Tooltip("バフの倍率(playerの該当のステータス*buff late)")] float _buffLate = 1;
    
    public float SkillTime => _skillTime;
    public float SkillCoolTime => _skillCoolTime;
    public BuffKind BuffKind => _buffKind;
    public float BuffLate => _buffLate;
}
