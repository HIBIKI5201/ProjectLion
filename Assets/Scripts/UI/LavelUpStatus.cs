using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class LavelUpStatus : UIElement_B
{
    Label _labelText;
    VisualElement _icon;

    [UxmlAttribute]
    public string aaaa;
    [UxmlAttribute]
    public Texture2D testTexture;

    public LavelUpStatus() : base("UXML/LevelUpStatus.uxml") { }

    protected override Task Initialize_S(TemplateContainer container)
    {
        _icon = container.Q<VisualElement>("StatusIcon");
        _labelText = container.Q<Label>("StatusName");

        if(testTexture)
            _icon.style.backgroundImage = testTexture;
        _labelText.text = aaaa;

        var levelUpStatus = container.Q<Label>("root");
        levelUpStatus.AddManipulator(new Clickable(() => OnClick()));
        return Task.CompletedTask;
        void OnClick()
        {

        }
    }
}
