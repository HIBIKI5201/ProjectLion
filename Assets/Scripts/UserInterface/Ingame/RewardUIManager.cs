using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class RewardUIManager : UIElement_B
{
    public RewardUIManager() : base(""){ }

    VisualElement _expSlider;
    Label _expText;
    Label _goldText;

    protected override Task Initialize_S(TemplateContainer container)
    {
        
        return Task.CompletedTask;
    }
    public void ChangeGold(int gold) => _goldText.text = gold.ToString();
    public void ChangeEXP(float currentExp, float nextLevelExp)
    {
        _expText.text= currentExp.ToString();
    }
}
