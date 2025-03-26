using SymphonyFrameWork.CoreSystem;
using UnityEngine;

public class DropItemManager : MonoBehaviour
{
    private PlayerController _player;
    private NormalShootManager _shooter;

    private float _timer = 0;

    [Header("アイテムの設定")]

    [Tooltip("アイテムの種類")][SerializeField]
    private ItemWeaponType _weaponType;
    public ItemWeaponType WeaponType { get => _weaponType; }    
    [Tooltip("アイテムの生存時間")][SerializeField]
    private float _itemLifeTime = 10;

    [Header("回復アイテムの設定")]

    [Tooltip("回復量")][SerializeField]
    private float _healpoint = 0;
    public float Healpoint { get => _healpoint; }


    public void Initialize()
    {
        _player = SingletonDirector.GetSingleton<PlayerController>();
        _shooter = _player.GetComponentInChildren<NormalShootManager>();
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ItemPicUp(this);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// アイテムを拾った際に呼び出す処理
    /// </summary>
    /// <param name="dropItem"></param>
    private void ItemPicUp(DropItemManager dropItem)
    {
        switch (dropItem.WeaponType)
        {
            case DropItemManager.ItemWeaponType.Weapon1Item:
                _shooter.SetWeapon(0);
                break;
            case DropItemManager.ItemWeaponType.Weapon2Item:
                _shooter.SetWeapon(1);
                break;
            case DropItemManager.ItemWeaponType.Weapon3Item:
                _shooter.SetWeapon(2);
                break;
            case DropItemManager.ItemWeaponType.HealItem:
                Debug.Log($"回復アイテムを拾いました{_player.CurrentHealth}");

                if (_player.MaxHealth >= _player.CurrentHealth + dropItem.Healpoint)
                {
                    _player.AddDamage(-dropItem.Healpoint);
                    Debug.Log($"回復しました{_player.CurrentHealth}");
                }
                else
                {
                    _player.AddDamage(_player.CurrentHealth - _player.MaxHealth);
                    Debug.Log($"回復しました MaxHP{_player.CurrentHealth}");
                }
                break;
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
