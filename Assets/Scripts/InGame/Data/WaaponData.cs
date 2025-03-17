using UnityEngine;

[System.Serializable]
public class WeaponData
{
    [Header("’eŠÛ‚ÌÝ’è")]

    [Tooltip("’e‘¬")]
    [SerializeField]
    private float _bulletSpeed = 5;
    public float BulletSpeed { get => _bulletSpeed; }
    [Tooltip("’eŠÛ‚ÌŽ‘±ŽžŠÔ")]
    [SerializeField]
    private float _bulletDuration = 2;
    public float BulletDuration { get => _bulletDuration; }
    [Tooltip("UŒ‚—Í‚Ì”{—¦")]
    [SerializeField]
    private float _attackPowerMultiplier = 1;
    public float AttackPowerMultiplier { get => _attackPowerMultiplier; }
    [Tooltip("ŽË’ö”ÍˆÍ")]
    [SerializeField]
    private float _attackRange = 1;
    public float AttackRange { get => _attackRange; }
    [Tooltip("UŒ‚‘¬“x")]
    [SerializeField]
    private float _attackSpeed = 1;
    public float AttackSpeed { get => _attackSpeed; }
}