using SymphonyFrameWork.CoreSystem;
using UnityEngine;

public class PickUpManager : MonoBehaviour
{
    private PlayerController _player;
    private NormalShootManager _shooter;

    private void Start()
    {
        _player = SingletonDirector.GetSingleton<PlayerController>();
        _shooter = GetComponent<NormalShootManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out DropItemManager dropItem))
        {
            ItemPicUp(dropItem);
            Destroy(collision.gameObject);
        }
    }

    /// <summary>
    /// �A�C�e�����E�����ۂɌĂяo������
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
                Debug.Log("�񕜃A�C�e�����E���܂���");
                _player.AddDamage(-dropItem.Healpoint);
                break;
        }
    }
}

