using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class ExpSlider : UIElement_B
{
    public ExpSlider() : base("UITK_Items/UXML/InGame/SliderBar") { }

    [UxmlAttribute]
    Texture2D _coverTexture;

    [UxmlAttribute]
    Color _color;

    [UxmlAttribute]
    bool _isReversed;

    [UxmlAttribute, Range(0, 1)]
    float _defaltSliderPercent;

    VisualElement _slider;
    protected override Task Initialize_S(TemplateContainer container)
    {
        var coverElement = container.Q<VisualElement>("CoverTexture");
        coverElement.style.backgroundImage = _coverTexture;

        _slider = container.Q<VisualElement>("Slider");
        _slider.style.color = _color;
        if (_isReversed)
        {
            _slider.style.alignSelf = Align.FlexStart;
        }
        else
        {
            _slider.style.alignSelf = Align.FlexEnd;
        }

        OnSliderChange(_defaltSliderPercent);
        return Task.CompletedTask;
    }
    /// <summary>
    /// 正規化された値を基にsliderの値を更新します
    /// </summary>
    /// <param name="normalize">sliderに設定する正規化された値</param>
    public void OnSliderChange(float normalize)
    {
        if (normalize > 100 && normalize < 0)
        {
            Debug.LogWarning("正規化された値を代入してください");
        }
        _slider.style.width = Length.Percent(normalize * 100);
    }
}
