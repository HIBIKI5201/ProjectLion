using SymphonyFrameWork.CoreSystem;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class InGameUIManager : MonoBehaviour
{
    UIDocument _document;
    VisualElement _root;

    [SerializeField] MastorData _uiDatas;
    [SerializeField] LevelUpManager _levelUpManager;
    [SerializeField] LevelContainer _levelContainer;
    [SerializeField] InGameManager _inGameManager;
    PlayerController _playerController;
    private async void Start()
    {
        _document = GetComponent<UIDocument>();
        if (!_document)
        {
            Debug.LogWarning("document is null");
            return;
        }
        _root = _document.rootVisualElement;

        _playerController = SingletonDirector.GetSingleton<PlayerController>();

        await PanelSetting();
    }
    Task PanelSetting()
    {
        LevelUpPanel panel = _root.Q<LevelUpPanel>("LevelUpPanel");
        if (panel != null)
        {
            _levelUpManager.OnLevelChanged += (x, y) => panel.OnLevelUp(x, y, _uiDatas);
        }
        else
        {
            Debug.Log("panel is null");
        }

        var timerText = _root.Q<TimerUI>("TimerUI");
        _inGameManager.OnTimerChanged += x => timerText.OnTimerChanged(x / 60, x % 60);

        var status = _root.Q<StatusUIManager>("StatusUIManager");
        if (status is null) Debug.Log("statusUIManager is null");
        _levelUpManager.OnGetItem += () => status?.OnStatusChange(LevelUpManager.ItemHaveValue);

        var reward = _root.Q<RewardUIManager>("RewardUI");
        _levelContainer.OnAddExperiance += (x, y) => reward.ChangeEXP(x, y);

        return Task.CompletedTask;
    }
}
