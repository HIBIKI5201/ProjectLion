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
    async Task PanelSetting()
    {
        LevelUpPanel panel = _root.Q<LevelUpPanel>("LevelUpPanel");
        await panel.InitializeTask;
        if (panel != null)
        {
            _levelUpManager.OnLevelChanged += (x, y) => panel.OnLevelUp(x, y, _uiDatas);
        }
        else
        {
            Debug.Log("panel is null");
        }

        TimerUI timerText = _root.Q<TimerUI>("TimerUI");
        await timerText.InitializeTask;
        _inGameManager.OnTimerChanged += x => timerText.OnTimerChanged(x / 60, x % 60);

        StatusUIManager status = _root.Q<StatusUIManager>("StatusUIManager");
        await status.InitializeTask;
        if (status is null) Debug.Log("statusUIManager is null");
        _levelUpManager.OnGetItem += () => status?.OnStatusChange(LevelUpManager.ItemHaveValue);

        RewardUIManager reward = _root.Q<RewardUIManager>("RewardUI");
        await reward.InitializeTask;
        _levelContainer.OnAddExperiance += (x, y) => reward.ChangeEXP(x, y);
    }
}
