using SymphonyFrameWork.CoreSystem;
using UnityEngine;

public class DropItemManager : MonoBehaviour
{
    private PlayerController _player;

    private float _timer = 0;

    [Header("アイテムの設定")]

    [Tooltip("アイテムの種類")][SerializeField]
    private ItemWeaponType _weaponType;
    public ItemWeaponType WeaponType { get => _weaponType; }    
    [Tooltip("アイテムの生存時間")][SerializeField]
    private float _itemLifeTime = 10;


    //[Header("弾丸の設定")]

    //[Tooltip("弾速")]
    //[SerializeField]
    //private float _bulletSpeed = 5;
    //public float BulletSpeed { get => _bulletSpeed; }
    //[Tooltip("弾丸の持続時間")]
    //[SerializeField]
    //private float _bulletDuration = 2;
    //public float BulletDuration { get => _bulletDuration; }
    //[Tooltip("攻撃力の倍率")]
    //[SerializeField]
    //private float _attackPowerMultiplier = 1;
    //public float AttackPowerMultiplier { get => _attackPowerMultiplier; }
    //[Tooltip("射程範囲")]
    //[SerializeField]
    //private float _attackRange = 1;
    //public float AttackRange { get => _attackRange; }
    //[Tooltip("攻撃速度")]
    //[SerializeField]
    //private float _attackSpeed = 1;
    //public float AttackSpeed { get => _attackSpeed; }


    [Header("回復アイテムの設定")]

    [Tooltip("回復量")][SerializeField]
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
            Debug.Log($"{_weaponType} が消滅しました");
            Destroy(gameObject);
        }
    }

    public enum ItemWeaponType
    {
        Weapon1Item,
        Weapon2Item,
        Weapon3Item,
        //回復アイテムは別処理
        HealItem
    }
}
