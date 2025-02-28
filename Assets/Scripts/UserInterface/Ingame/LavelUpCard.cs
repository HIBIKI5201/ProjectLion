using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class LavelUpCard : UIElement_B
{
    [UxmlAttribute] string testText;
    [UxmlAttribute] Texture2D testIcon;

    Label _statusName;
    Label _information;
    VisualElement _icon;

    public Action OnLevelUpChoiced;

    public LavelUpCard() : base("UITK_Items/UXML/InGame/LevelUpStatus") { }

    protected override Task Initialize_S(TemplateContainer container)
    {
        _icon = container.Q<VisualElement>("StatusIcon");
        _statusName = container.Q<Label>("StatusName");
        _information = container.Q<Label>("StatusInformation");

        if (testIcon)
            _icon.style.backgroundImage = testIcon;
        _statusName.text = testText;

        var levelUpStatus = container.Q<Label>("root");
        container.AddManipulator(new Clickable(() => OnClick()));
        return Task.CompletedTask;
        void OnClick()
        {
            Debug.Log($"OnClick");
            OnLevelUpChoiced?.Invoke();
        }
    }
    public void ChangeStyle(Texture2D icon, string name, string info)
    {
        _statusName.text = name;
        _information.text = info;
        _icon.style.backgroundImage = icon;
    }
}
