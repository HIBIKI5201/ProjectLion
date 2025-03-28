using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class WeaponUI : UIElement_B
{
    [UxmlAttribute] private WeaponUIDataList _data;//ToDo:Here 武器のアイコン、テキストを格納しておくためのものを入れる
    private VisualElement _icon;
    private Label _text;
    
    public WeaponUI() : base("UITK_Items/UXML/InGame/Weapon"){}

    protected override Task Initialize_S(TemplateContainer container)
    {
        _icon = container.Q<VisualElement>("WeaponTexture");
        _text = container.Q<Label>("WeaponLabel");

        if (_data is not null)
        {
            _icon.style.backgroundImage = _data.DetaList[0].Icon;
            _text.text = _data.DetaList[0].Info;
        }
        
        return Task.CompletedTask;
    }

    public void ChangeWeaponUI(WeaponData weapondata)
    {
        var data = _data.GetWeaponUIData(weapondata);
        if(data == null) return;
        
        Debug.Log(data);
        _icon.style.backgroundImage = data.Icon;
        _text.text = data.Info;
    }
}
