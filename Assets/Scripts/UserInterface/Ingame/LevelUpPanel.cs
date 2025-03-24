using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class LevelUpPanel : UIElement_B
{
    List<LavelUpCard> _levelUpPanel = new();
    public LevelUpPanel() : base("UITK_Items/UXML/InGame/LevelUpPanel") { }
    protected override Task Initialize_S(TemplateContainer container)
    {
        this.style.visibility = Visibility.Hidden;
        _levelUpPanel = container.Query<LavelUpCard>().ToList();
        return Task.CompletedTask;
    }

    public void OnLevelUp(IEnumerable<ItemKind> kinds, Action<ItemKind> callBack, ItemUIDataList uiDatasList)
    {
        if (kinds.Count() != _levelUpPanel.Count)//�G���[���N���Ȃ��悤�ɗ����̗v�f�̌����m�F
        {
            Debug.LogWarning($"kinds not equal _stateusPanel\nkinds:{kinds.Count()}, StateusPanel:{_levelUpPanel.Count}");
            callBack?.Invoke(0);
            return;
        }

        this.style.visibility = Visibility.Visible;

        for (var i = 0; i < _levelUpPanel.Count(); i++)
        {
            //�����ڂ̕ύX.
            var uiData = uiDatasList.FindEnum(kinds.ElementAt(i));
            _levelUpPanel[i].ChangeStyle(uiData.Texture, uiData.Name, uiData.Information);

            //callback
            int index = i;//�N���[�W��
            _levelUpPanel[i].OnLevelUpChoiced += () =>
            {
                callBack?.Invoke(kinds.ElementAt(index));
                this.style.visibility = Visibility.Hidden;
                ResetEvent();
            };
        }
    }
    void ResetEvent()
    {
        foreach (var item in _levelUpPanel)
        {
            item.OnLevelUpChoiced = null;
        }
    }
}
