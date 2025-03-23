using SymphonyFrameWork.CoreSystem;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class InGameUIManager : MonoBehaviour
{
    UIDocument _document;
    VisualElement _root;

    [SerializeField] MastorData _uiDatas;
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
        LevelUpPanel panel = _root.Q<LevelUpPanel>();
        TimerUI timerText = _root.Q<TimerUI>();
        StatusUIManager status = _root.Q<StatusUIManager>();
        SkillUI skill = _root.Q<SkillUI>();
        RewardUIManager reward = _root.Q<RewardUIManager>();
        ResultPanel result = _root.Q<ResultPanel>();

        var levelUpManager = FindFirstObjectByType<LevelUpManager>();
        var inGameManager = FindFirstObjectByType<InGameManager>();
        var specialAttackManager = FindFirstObjectByType<SpecialAttackManager>();
        var levelContainer = FindFirstObjectByType<LevelContainer>();

        await Task.WhenAll(
            panel.InitializeTask,
            timerText.InitializeTask,
            status.InitializeTask,
            skill.InitializeTask,
            reward.InitializeTask);

        levelUpManager.OnLevelChanged += (kind, action) => panel.OnLevelUp(kind, action, _uiDatas);
        levelUpManager.OnGetItem += () => status?.OnStatusChange(LevelUpManager.ItemHaveValue);
        inGameManager.OnTimerChanged += x => timerText.OnTimerChanged(x / 60, x % 60);
        specialAttackManager.SpecialEvant += skill.ChangeSkillGage;
        skill.OnUseUltimate += specialAttackManager.SpecialAttack;
        levelContainer.OnAddExperiance += reward.ChangeEXP;
        _playerController.OnDeath += () => result.ActivateResultPanel(inGameManager.EnemyKillCount);
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
