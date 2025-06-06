using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class SkillUI : UIElement_B
{
    [UxmlAttribute, Range(0, 1)] float _normal;

    VisualElement _gage;
    VisualElement _gageMask;
    VisualElement _cover;

    public event Action OnUseUltimate;

    public SkillUI() : base("UITK_Items/UXML/InGame/SpecialAttack")
    {
    }


    protected override Task Initialize_S(TemplateContainer container)
    {
        _gage = container.Q<VisualElement>("Gage");
        _gageMask = container.Q<VisualElement>("GageMask");
        _cover = container.Q<VisualElement>("Cover");
        _gageMask.style.overflow = Overflow.Hidden;

        ChangeSkillGage(0, 1);

        container.AddManipulator(new Clickable(() => OnClick()));
        return Task.CompletedTask;

        void OnClick()
        {
            Debug.Log("use ultimate");
            OnUseUltimate?.Invoke();
        }
    }

    public void ChangeSkillGage(float current, float next)
    {
        if (current >= next)
        {
            current = next; 
            _cover.style.visibility = Visibility.Hidden;
        }
        else
        {
            _cover.style.visibility = Visibility.Visible;
        }

        var normalizeSkillGage = 100 - (float)current / next * 100;

        _gage.style.bottom = Length.Percent(normalizeSkillGage);
        _gageMask.style.top = Length.Percent(normalizeSkillGage);
    }
}