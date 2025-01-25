using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class SpecialAttackUI : UIElement_B
{
    public event Action OnUseUltimate;
    public SpecialAttackUI() : base("UXML/SpecialAttack.uxml") { }

    protected override Task Initialize_S(TemplateContainer container)
    {
        container.AddManipulator(new Clickable(() => OnClick()));
        return Task.CompletedTask;
        void OnClick()
        {
            Debug.Log("use ultimate");
            OnUseUltimate?.Invoke();
        }
    }
}
