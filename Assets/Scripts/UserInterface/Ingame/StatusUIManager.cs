using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class StatusUIManager : UIElement_B
{
    [UxmlAttribute]
    MastorData _uiData;
    public StatusUIManager() : base("UITK_Items/UXML/InGame/Status") { }

    Dictionary<ItemKind, StatusUI> _statusPanels = new Dictionary<ItemKind, StatusUI>();
    protected override async Task Initialize_S(TemplateContainer container)
    {
        #region VisualElement�擾
        _statusPanels.Add(ItemKind.HealthUp, container.Q<StatusUI>("Health"));
        _statusPanels.Add(ItemKind.AttackUp, container.Q<StatusUI>("AttackPower"));
        _statusPanels.Add(ItemKind.AttackSpeedUp, container.Q<StatusUI>("AttackSpeed"));
        _statusPanels.Add(ItemKind.AttackRangeUp, container.Q<StatusUI>("AttackRange"));
        _statusPanels.Add(ItemKind.ExperianceUp, container.Q<StatusUI>("ExperianceUp"));
        _statusPanels.Add(ItemKind.AgilityUp, container.Q<StatusUI>("Agility"));
        _statusPanels.Add(ItemKind.SkillPowerUp, container.Q<StatusUI>("SkillPower"));
        _statusPanels.Add(ItemKind.SkillCoolTimeDown, container.Q<StatusUI>("SkillCoolTime"));
        _statusPanels.Add(ItemKind.HealOfTime, container.Q<StatusUI>("HealthRegen"));
        //_statusPanels.Add(ItemKind.KnockBackUp, container.Q<StatusUI>("KnockBack"));
        #endregion

        Task[] tasks = _statusPanels.Select(kvp => kvp.Value.InitializeTask).ToArray();
        await Task.WhenAll(tasks);

        //status�̃A�C�R���ݒ�
        foreach (var item in _statusPanels)
        {
            item.Value.SetIcon(_uiData.FindEnum(item.Key).Texture, Color.white);
            item.Value.OnStatusChanged(0);
        }
    }

    public void OnStatusChange(Dictionary<ItemKind, int> itemDic)
    {
        foreach (ItemKind item in Enum.GetValues(typeof(ItemKind)))
        {
            if (_statusPanels.TryGetValue(item, out StatusUI statusPanel))
                statusPanel.OnStatusChanged(itemDic[item]);
        }
    }
}
