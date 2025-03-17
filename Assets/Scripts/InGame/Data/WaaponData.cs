using UnityEngine;

[System.Serializable]
public class WeaponData
{
    [Header("�e�ۂ̐ݒ�")]

    [Tooltip("�e��")]
    [SerializeField]
    private float _bulletSpeed = 5;
    public float BulletSpeed { get => _bulletSpeed; }
    [Tooltip("�e�ۂ̎�������")]
    [SerializeField]
    private float _bulletDuration = 2;
    public float BulletDuration { get => _bulletDuration; }
    [Tooltip("�U���͂̔{��")]
    [SerializeField]
    private float _attackPowerMultiplier = 1;
    public float AttackPowerMultiplier { get => _attackPowerMultiplier; }
    [Tooltip("�˒��͈�")]
    [SerializeField]
    private float _attackRange = 1;
    public float AttackRange { get => _attackRange; }
    [Tooltip("�U�����x")]
    [SerializeField]
    private float _attackSpeed = 1;
    public float AttackSpeed { get => _attackSpeed; }
}