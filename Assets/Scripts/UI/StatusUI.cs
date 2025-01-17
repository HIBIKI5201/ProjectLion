using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class StatusUI : UIElement_B
{
    [UxmlAttribute]
    Texture2D _icon;
    [UxmlAttribute]
    string _text;

    public StatusUI() : base("UXML/StatusElement") { }

    protected override Task Initialize_S(TemplateContainer container)
    {
        var icon = container.Q<VisualElement>("Icon");
        var text = container.Q<Label>("Text");

        if(icon != null)
            icon.style.backgroundImage = _icon;
        text.text = _text;

        return Task.CompletedTask;
    }
}
