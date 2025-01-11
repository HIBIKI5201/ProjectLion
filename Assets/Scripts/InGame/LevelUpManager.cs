using SymphonyFrameWork.CoreSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
public class LevelUpManager : MonoBehaviour
{
    public static readonly Dictionary<ItemKind, int> ItemHaveValue = new();
    private readonly int _itemKindValue = Enum.GetValues(typeof(ItemKind)).Length;
    private readonly Dictionary<ItemKind, int> _itemLimitDict = new()
    {
        { ItemKind.HealthUp, 30 },
        { ItemKind.AttackUp, 30 },
        { ItemKind.DefenseUp, 30 },
        { ItemKind.AgilityUp, 30 },
        { ItemKind.AttackSpeedUp, 30 },
        { ItemKind.AttackRangeUp, 30 },
        { ItemKind.SkillCoolTimeDown, 10 },
        { ItemKind.HealOfTime, 10 },
        { ItemKind.ExperianceUp, 10 },
        { ItemKind.NockBackUp, 10 },
        { ItemKind.SkillPowerUp, 10 },
        { ItemKind.GetCoinValueUp, 10 },
    };
    private void Start()
    {
        foreach (ItemKind kind in Enum.GetValues(typeof(ItemKind)))
            ItemHaveValue.Add(kind, 0);
    }

    public void GetNewItem()
    {
        HashSet<ItemKind> kinds = new();
        while (kinds.Count < 3)
        {
            int index = Random.Range(1, _itemKindValue);
            ItemKind kind = (ItemKind)Enum.GetValues(typeof(ItemKind)).GetValue(index);
            if (ItemHaveValue[kind] <= _itemLimitDict[kind])
                kinds.Add(kind);
        }
        Debug.Log($"level up and selected items are u{string.Join(" ", kinds)}v");
        AddItem(kinds.ElementAt(0));
    }

    private void AddItem(ItemKind kind)
    {
        switch (kind)
        {
            case ItemKind.HealthUp:
            case ItemKind.AttackUp:
            case ItemKind.DefenseUp:
            case ItemKind.AgilityUp:
            case ItemKind.AttackSpeedUp:
            case ItemKind.AttackRangeUp:
                PlayerController player = SingletonDirector.GetSingleton<PlayerController>();
                MobData data = new MobData(player.Data,
                    health: ItemHaveValue[ItemKind.HealthUp] * 0.1f * player.Data.MaxHealth,
                    attack: ItemHaveValue[ItemKind.AttackUp] * 0.1f * player.Data.Attack,
                    defense: ItemHaveValue[ItemKind.DefenseUp] * 0.1f * player.Data.Defense,
                    agility: ItemHaveValue[ItemKind.AgilityUp] * 0.1f * player.Data.Agility,
                    attackRange: ItemHaveValue[ItemKind.AttackRangeUp] * 0.1f * player.Data.AttackRange,
                    attackSpeed: ItemHaveValue[ItemKind.AttackSpeedUp] * 0.1f * player.Data.AttackSpeed);
                break;
        }
        ItemHaveValue[kind]++;
        Debug.Log($"choice kind is {kind}");
    }
}

public enum ItemKind
{
    None,

    HealthUp,
    AttackUp,
    DefenseUp,
    AgilityUp,
    AttackSpeedUp,
    AttackRangeUp,

    SkillCoolTimeDown,
    HealOfTime,
    ExperianceUp,
    NockBackUp,
    SkillPowerUp,
    GetCoinValueUp,
}