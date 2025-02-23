using SymphonyFrameWork.CoreSystem;
using System;
using System.Collections.Generic;
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
        //{ ItemKind.DefenseUp, 30 },
        { ItemKind.AgilityUp, 30 },
        { ItemKind.AttackSpeedUp, 30 },
        { ItemKind.AttackRangeUp, 30 },
        { ItemKind.SkillCoolTimeDown, 10 },
        { ItemKind.HealOfTime, 10 },
        { ItemKind.ExperianceUp, 10 },
        //{ ItemKind.KnockBackUp, 10 },
        { ItemKind.SkillPowerUp, 10 },
        //{ ItemKind.GetCoinValueUp, 10 },
    };

    private PlayerController player;
    private MobData data;
    private SpecialAttackManager specialMana;
    //private AutoHeal autoHeal;
    private LevelContainer levelContainer;
    private SpecialAttackSystem specialAttackSystem;

    public event Action<IEnumerable<ItemKind>, Action<ItemKind>> OnLevelChanged;
    public event Action OnGetItem;
    public event Action<Dictionary<ItemKind, int>> OnLevelChange;
    private void Start()
    {
        //ファインド使いすぎなのでいつかどうにかする
        foreach (ItemKind kind in Enum.GetValues(typeof(ItemKind)))
            ItemHaveValue.Add(kind, 0);
        player = SingletonDirector.GetSingleton<PlayerController>();
        data = player.Data;
        specialMana = FindAnyObjectByType<SpecialAttackManager>();
        //autoHeal = FindAnyObjectByType<AutoHeal>();
        levelContainer = FindAnyObjectByType<LevelContainer>();
        specialAttackSystem = FindAnyObjectByType<SpecialAttackSystem>();
    }

    [ContextMenu("LevelUp")]
    public void GetNewItem()
    {
        HashSet<ItemKind> kinds = new();
        while (kinds.Count < 3)//レベルが最大のアイテムの量によって無限ループの可能性あり
        {
            int index = Random.Range(1, _itemKindValue);
            ItemKind kind = (ItemKind)Enum.GetValues(typeof(ItemKind)).GetValue(index);
            if (ItemHaveValue[kind] <= _itemLimitDict[kind])
                kinds.Add(kind);
        }
        Debug.Log($"level up and selected items are 「{string.Join(" ", kinds)}」");
        OnLevelChanged?.Invoke(kinds, x => AddItem(x));
        //AddItem(kinds.ElementAt(0));
    }

    private void AddItem(ItemKind kind)
    {
        switch (kind)
        {
            case ItemKind.HealthUp:
                data = new MobData(player.Data,
                    health: ItemHaveValue[ItemKind.HealthUp] * 0.1f * player.Data.MaxHealth,
                    attack: 0,
                    defense: 0,
                    agility: 0,
                    attackRange: 0,
                    attackSpeed: 0);
                break;

            case ItemKind.AttackUp:
                data = new MobData(player.Data,
                    health: 0,
                    attack: ItemHaveValue[ItemKind.AttackUp] * 0.1f * player.Data.Attack,
                    defense: 0,
                    agility: 0,
                    attackRange: 0,
                    attackSpeed: 0);
                break;

            //case ItemKind.DefenseUp:

            case ItemKind.AgilityUp:
                data = new MobData(player.Data,
                    health: 0,
                    attack: 0,
                    defense: 0,
                    agility: ItemHaveValue[ItemKind.AgilityUp] * 0.1f * player.Data.Agility,
                    attackRange: 0,
                    attackSpeed: 0);
                break;

            case ItemKind.AttackSpeedUp:
                data = new MobData(player.Data,
                    health: 0,
                    attack: 0,
                    defense: 0,
                    agility: 0,
                    attackRange: 0,
                    attackSpeed: -ItemHaveValue[ItemKind.AttackSpeedUp] * 0.1f * player.Data.AttackSpeed);
                break;

            case ItemKind.AttackRangeUp:
                data = new MobData(player.Data,
                    health: 0,
                    attack: 0,
                    defense: 0,
                    agility: 0,
                    attackRange: ItemHaveValue[ItemKind.AttackRangeUp] * 0.1f * player.Data.AttackRange,
                    attackSpeed: 0);
                break;

            case ItemKind.SkillCoolTimeDown:
                specialMana.SpecialRequirePoint -= 0.02f * specialMana.InitRequirePoint;
                break;

            case ItemKind.HealOfTime:
                //autoHeal.Healvalue += 0.01f * autoHeal.InitHealValue;
                break;
            //以下二つの実装がスマートな感じな気がするから、時間があるタイミングで上二つも修正したい
            case ItemKind.ExperianceUp:
                levelContainer.ExperianceUp = ItemHaveValue[ItemKind.ExperianceUp];
                break;

            case ItemKind.SkillPowerUp:
                specialAttackSystem.Attackbuff = ItemHaveValue[ItemKind.SkillPowerUp];
                break;

        }
        player.LoadData(data);
        OnLevelChange?.Invoke(_itemLimitDict);

        ItemHaveValue[kind]++;
        OnGetItem?.Invoke();
        Debug.Log($"choice kind is {kind}");
    }
}

public enum ItemKind
{
    None,
    //以下ステータスバフ
    HealthUp,
    AttackUp,
    //DefenseUp,
    AgilityUp,
    AttackSpeedUp,
    AttackRangeUp,

    //以下特殊バフ
    SkillCoolTimeDown,
    HealOfTime,
    ExperianceUp,
    //KnockBackUp,
    SkillPowerUp,
    //GetCoinValueUp,
}