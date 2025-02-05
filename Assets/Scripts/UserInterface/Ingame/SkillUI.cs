using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class SkillUI : UIElement_B
{
    [UxmlAttribute, Range(0, 1)]
    float _normal;

    VisualElement _gage;
    VisualElement _gageMask;

    public event Action OnUseUltimate;
    public SkillUI() : base("UXML/SpecialAttack.uxml") { }


    protected override Task Initialize_S(TemplateContainer container)
    {

        _gage = container.Q<VisualElement>("Gage");
        _gageMask = container.Q<VisualElement>("GageMask");
        _gageMask.style.overflow = Overflow.Hidden;

        ChangeSkillGage(_normal, 1);

        container.AddManipulator(new Clickable(() => OnClick()));
        return Task.CompletedTask;
        void OnClick()
        {
            Debug.Log("use ultimate");
            OnUseUltimate?.Invoke();
        }
    }

    void ChangeSkillGage(float current, float next)
    {
        if (current / next > 1 && 0 > current / next)
        {
            Debug.LogWarning("ChagneSkillGage ‚Ì’l‚ª³‹K‰»‚³‚ê‚Ä‚¢‚Ü‚¹‚ñ");
        }
        _gage.style.bottom = Length.Percent(100 - current / next * 100);
        _gageMask.style.top = Length.Percent(100 - current / next * 100);
    }
}
