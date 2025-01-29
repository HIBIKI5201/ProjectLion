using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class StatusUI : UIElement_B
{
    [UxmlAttribute]
    Texture2D _defaltIcon;
    [UxmlAttribute]
    Color _defaltColor;
    [UxmlAttribute]
    float _defaltData;

    VisualElement _icon;
    Label _text;

    public StatusUI() : base("UXML/StatusElement") { }

    protected override Task Initialize_S(TemplateContainer container)
    {
        _icon = container.Q<VisualElement>("Icon");
        _text = container.Q<Label>("Text");


        return Task.CompletedTask;
    }
    public void SetIcon(Texture2D icon,Color color)
    {
        if (_icon != null)
        {
            _icon.style.backgroundImage = icon;
            _icon.style.color = color;
        }
    }
    public void OnStatusChanged(float status)
    {
        if (_text != null)
        {
            _text.text = status.ToString();
        }
    }
}
