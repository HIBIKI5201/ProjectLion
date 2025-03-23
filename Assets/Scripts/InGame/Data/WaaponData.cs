using UnityEngine;

[System.Serializable]
public class WeaponData
{
    [Header("弾丸の設定")]

    [Tooltip("弾速")]
    [SerializeField]
    private float _bulletSpeed = 5;
    public float BulletSpeed { get => _bulletSpeed; }
    [Tooltip("弾丸の持続時間")]
    [SerializeField]
    private float _bulletDuration = 2;
    public float BulletDuration { get => _bulletDuration; }
    [Tooltip("攻撃力の倍率")]
    [SerializeField]
    private float _attackPowerMultiplier = 1;
    public float AttackPowerMultiplier { get => _attackPowerMultiplier; }
    [Tooltip("射程範囲倍率")]
    [SerializeField]
    private float _attackRangeMultiplier = 1;
    public float AttackRangeMultiplier { get => _attackRangeMultiplier; }
    [Tooltip("攻撃頻度の修正値、クールタイムをこの変数の値で割ります")]
    [SerializeField]
    private float _attackCoolTimeModifier = 1;
    public float AttackCoolTimeModifier { get => _attackCoolTimeModifier; }
}