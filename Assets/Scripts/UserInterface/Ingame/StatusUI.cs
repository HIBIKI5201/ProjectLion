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

    //ToDo:��
    Texture2D _iconTexture;
    float _InitText;
    public StatusUI() : base("UXML/StatusElement") { }

    protected override Task Initialize_S(TemplateContainer container)
    {
        _icon = container.Q<VisualElement>("Icon");
        _text = container.Q<Label>("Text");

        SetIcon(_iconTexture, Color.white);
        OnStatusChanged(_InitText);

        return Task.CompletedTask;
    }
    public void SetIcon(Texture2D icon,Color color)
    {
        _iconTexture = icon;
        if (_icon != null)
        {
            _icon.style.backgroundImage = icon;
            _icon.style.color = color;
        }
    }
    public void OnStatusChanged(float status)
    {
        _InitText = status;
        if (_text != null)
        {
            _text.text = status.ToString();
        }
    }
}
