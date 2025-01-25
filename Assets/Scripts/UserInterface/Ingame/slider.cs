using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class SliderManager : UIElement_B
{
    public SliderManager() : base("UXML/Slider.uxml") { }

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
    /// ���K�����ꂽ�l�����slider�̒l���X�V���܂�
    /// </summary>
    /// <param name="normalize">slider�ɐݒ肷�鐳�K�����ꂽ�l</param>
    public void OnSliderChange(float normalize)
    {
        if (normalize > 100 && normalize < 0)
        {
            Debug.LogWarning("���K�����ꂽ�l�������Ă�������");
        }
        _slider.style.width = Length.Percent(normalize * 100);
    }
}
