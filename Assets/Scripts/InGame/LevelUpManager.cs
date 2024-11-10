using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
public class LevelUpManager : MonoBehaviour
{
    readonly int _itemKindValue = Enum.GetValues(typeof(ItemKind)).Length;
    [ContextMenu("GetNewItem")]
    public void GetNewItem()
    {
        HashSet<int> indexs = new();
        while (indexs.Count < 3)
            indexs.Add(Random.Range(1, _itemKindValue));

        ItemKind[] kinds = indexs.Select(n => (ItemKind) Enum.GetValues(typeof(ItemKind)).GetValue(n)).ToArray();
        foreach (var kind in kinds)
            AddItem(kind);
    }

    private void AddItem(ItemKind kind)
    {

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