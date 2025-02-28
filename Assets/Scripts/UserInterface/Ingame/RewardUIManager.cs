using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class RewardUIManager : UIElement_B
{
    public RewardUIManager() : base("UITK_Items/UXML/InGame/EXPBar") { }

    ExpSlider _expSlider;
    Label _expText;
    Label _goldText;

    protected override async Task Initialize_S(TemplateContainer container)
    {
        _expText=container.Q<Label>("EXP");
        _goldText = container.Q<Label>("Coin");
        _expSlider = container.Q<ExpSlider>("ExpSlider");

        await _expSlider.InitializeTask;

        _expSlider.OnSliderChange(0);
        _expText.text = "0";
    }
    public void ChangeGold(int gold) => _goldText.text = gold.ToString();
    public void ChangeEXP(float currentExp, float nextLevelExp)
    {
        _expText.text= currentExp.ToString();
        _expSlider.OnSliderChange(currentExp / nextLevelExp);
    }
}
