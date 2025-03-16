using SymphonyFrameWork.CoreSystem;
using UnityEngine;

public class DropItemManager : MonoBehaviour
{
    private PlayerController _player;

    private float _timer = 0;

    [Header("�A�C�e���̐ݒ�")]

    [Tooltip("�A�C�e���̎��")][SerializeField]
    private ItemWeaponType _weaponType;
    public ItemWeaponType WeaponType { get => _weaponType; }    
    [Tooltip("�A�C�e���̐�������")][SerializeField]
    private float _itemLifeTime = 10;


    //[Header("�e�ۂ̐ݒ�")]

    //[Tooltip("�e��")]
    //[SerializeField]
    //private float _bulletSpeed = 5;
    //public float BulletSpeed { get => _bulletSpeed; }
    //[Tooltip("�e�ۂ̎�������")]
    //[SerializeField]
    //private float _bulletDuration = 2;
    //public float BulletDuration { get => _bulletDuration; }
    //[Tooltip("�U���͂̔{��")]
    //[SerializeField]
    //private float _attackPowerMultiplier = 1;
    //public float AttackPowerMultiplier { get => _attackPowerMultiplier; }
    //[Tooltip("�˒��͈�")]
    //[SerializeField]
    //private float _attackRange = 1;
    //public float AttackRange { get => _attackRange; }
    //[Tooltip("�U�����x")]
    //[SerializeField]
    //private float _attackSpeed = 1;
    //public float AttackSpeed { get => _attackSpeed; }


    [Header("�񕜃A�C�e���̐ݒ�")]

    [Tooltip("�񕜗�")][SerializeField]
    private float _healpoint = 0;
    public float Healpoint { get => _healpoint; }


    public void Initialize()
    {
        _player = SingletonDirector.GetSingleton<PlayerController>();
        _timer = Time.time;
    }


    private void Update()
    {
        if (_itemLifeTime + _timer < Time.time)
        {
            Debug.Log($"{_weaponType} �����ł��܂���");
            Destroy(gameObject);
        }
    }

    public enum ItemWeaponType
    {
        Weapon1Item,
        Weapon2Item,
        Weapon3Item,
        //�񕜃A�C�e���͕ʏ���
        HealItem
    }
}
