using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class LavelUpStatus : UIElement_B
{
    Label _labelText;
    VisualElement _icon;
    public LavelUpStatus():base("Assets/Resources_moved/UITK_Items/UXML/InGame/LevelUpStatus.uxml")
    {
    }
    
    protected override Task Initialize_S(TemplateContainer container)
    {
        _icon=container.Q<VisualElement>("StatusIcon");
        _labelText = container.Q<Label>("StatusName");

        var levelUpStatus = container.Q<Label>("root");
        levelUpStatus.AddManipulator(new Clickable(() => OnClick()));
        return Task.CompletedTask;
        void OnClick()
        {

        }
    }
}
