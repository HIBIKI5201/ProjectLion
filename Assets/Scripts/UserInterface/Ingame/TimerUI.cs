using System.Threading.Tasks;
using UnityEngine.UIElements;

[UxmlElement]
public partial class TimerUI : UIElement_B
{
    public TimerUI() : base("UITK_Items/UXML/InGame/Timer") { }
    Label _timer;
    protected override Task Initialize_S(TemplateContainer container)
    {
        _timer = container.Q<Label>("TimerText");
        return Task.CompletedTask;
    }
    public void OnTimerChanged(float minutes, float seconds)
    {
        if (_timer != null)
        {
            _timer.text = $"{minutes.ToString("00")}:{seconds.ToString("00")}";
        }
    }
}
