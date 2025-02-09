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
    public LevelUpPanel() : base("UXML/LevelUpPanel.uxml") { }
    protected override Task Initialize_S(TemplateContainer container)
    {
        this.style.visibility = Visibility.Hidden;
        _levelUpPanel = container.Query<LavelUpCard>().ToList();
        return Task.CompletedTask;
    }

    public void OnLevelUp(IEnumerable<ItemKind> kinds, Action<ItemKind> callBack,MastorData uiDatas)
    {
        if (kinds.Count() != _levelUpPanel.Count)//エラーが起きないように両方の要素の個数を確認
        {
            Debug.LogWarning($"kinds not equal _stateusPanel\nkinds:{kinds.Count()}, StateusPanel:{_levelUpPanel.Count}");
            callBack?.Invoke(0);
            return;
        }

        this.style.visibility = Visibility.Visible;

        for (var i = 0; i < _levelUpPanel.Count(); i++)
        {
            //見た目の変更.
            var uiData = uiDatas.FindEnum(kinds.ElementAt(i));
            _levelUpPanel[i].ChangeStyle(uiData.Texture, uiData.Name, uiData.Information);

            //callback
            int index = i;//クロージャ
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
