using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class RewardUIManager : UIElement_B
{
    public RewardUIManager() : base("UXML/EXPBar.uxml") { }

    ExpSlider _expSlider;
    Label _expText;
    Label _goldText;

    protected override Task Initialize_S(TemplateContainer container)
    {
        _expText=container.Q<Label>("EXP");
        _goldText = container.Q<Label>("Coin");
        _expSlider = container.Q<ExpSlider>("ExpSlider");

        _expSlider.OnSliderChange(0);
        _expText.text = "0";

        return Task.CompletedTask;
    }
    public void ChangeGold(int gold) => _goldText.text = gold.ToString();
    public void ChangeEXP(float currentExp, float nextLevelExp)
    {
        _expText.text= currentExp.ToString();
        _expSlider.OnSliderChange(currentExp / nextLevelExp);
    }
}
