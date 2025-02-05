using SymphonyFrameWork.CoreSystem;
using System.Threading.Tasks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class InGameUIManager : MonoBehaviour
{
    UIDocument _document;
    VisualElement _root;

    [SerializeField] MastorData _uiDatas;
    [SerializeField] LevelUpManager _levelUpManager;
    [SerializeField] LevelContainer _levelContainer;
    [SerializeField] InGameManager _inGameManager;
    [SerializeField] SpecialAttackManager _specialAttackManager;
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
        TimerUI timerText = _root.Q<TimerUI>("TimerUI");
        StatusUIManager status = _root.Q<StatusUIManager>("StatusUIManager");
        SkillUI skill = _root.Q<SkillUI>("SkillUI");
        RewardUIManager reward = _root.Q<RewardUIManager>("RewardUI");

        Task[] wait = new Task[] 
        {
            panel.InitializeTask,
            timerText.InitializeTask,
            status.InitializeTask,
            skill.InitializeTask,
            reward.InitializeTask,
        };

        await Task.WhenAll(wait);

        _levelUpManager.OnLevelChanged += (x, y) => panel.OnLevelUp(x, y, _uiDatas);
        _inGameManager.OnTimerChanged += x => timerText.OnTimerChanged(x / 60, x % 60);
        _levelUpManager.OnGetItem += () => status?.OnStatusChange(LevelUpManager.ItemHaveValue);
        //_specialAttackManager.SpecialEvant += x => skill.ChangeSkillGage(
        //    _specialAttackManager.SpecialExperiancePoint,
        //    _specialAttackManager.SpecialRequirePoint);
        skill.OnUseUltimate += () => _specialAttackManager.SpecialAttack();
        _levelContainer.OnAddExperiance += (x, y) => reward.ChangeEXP(x, y);
    }
    //private void OnDisable()
    //{
    //    LevelUpPanel panel = _root.Q<LevelUpPanel>("LevelUpPanel");
    //    TimerUI timerText = _root.Q<TimerUI>("TimerUI");
    //    StatusUIManager status = _root.Q<StatusUIManager>("StatusUIManager");
    //    SkillUI skill = _root.Q<SkillUI>("");
    //    RewardUIManager reward = _root.Q<RewardUIManager>("RewardUI");

    //    _levelUpManager.OnLevelChanged -= (x, y) => panel.OnLevelUp(x, y, _uiDatas);
    //    _inGameManager.OnTimerChanged -= x => timerText.OnTimerChanged(x / 60, x % 60);
    //    _levelUpManager.OnGetItem -= () => status?.OnStatusChange(LevelUpManager.ItemHaveValue);
        //_specialAttackManager.SpecialEvant -= x => skill.ChangeSkillGage(
        //    _specialAttackManager.SpecialExperiancePoint,
        //    _specialAttackManager.SpecialRequirePoint);
    //    skill.OnUseUltimate -= () => _specialAttackManager.SpecialAttack();
    //    _levelContainer.OnAddExperiance -= (x, y) => reward.ChangeEXP(x, y);
    //}
}
