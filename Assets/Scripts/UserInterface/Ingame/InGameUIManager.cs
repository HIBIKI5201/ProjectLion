using SymphonyFrameWork.CoreSystem;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class InGameUIManager : MonoBehaviour
{
    UIDocument _document;
    VisualElement _root;

    [SerializeField] ItemUIDataList _uiDatasList;
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
        WeaponUI weapon = _root.Q<WeaponUI>();

        var levelUpManager = FindFirstObjectByType<LevelUpManager>();
        var inGameManager = FindFirstObjectByType<InGameManager>();
        var specialAttackManager = FindFirstObjectByType<SpecialAttackManager>();
        var levelContainer = FindFirstObjectByType<LevelContainer>();

        await Task.WhenAll(
            panel.InitializeTask,
            timerText.InitializeTask,
            status.InitializeTask,
            skill.InitializeTask,
            reward.InitializeTask,
            result.InitializeTask
            //weapon.InitializeTask
            );

        levelUpManager.OnLevelChanged += (kind, action) => panel.OnLevelUp(kind, action, _uiDatasList);
        levelUpManager.OnGetItem += () => status?.OnStatusChange(LevelUpManager.ItemHaveValue);
        inGameManager.OnTimerChanged += x => timerText.OnTimerChanged(x / 60, x % 60);
        specialAttackManager.SpecialEvant += skill.ChangeSkillGage;
        skill.OnUseUltimate += specialAttackManager.SpecialAttack;
        levelContainer.OnAddExperiance += reward.ChangeEXP;
        _playerController.OnDeath += () => result.ActivateResultPanel(inGameManager.EnemyKillCount);
        _playerController.transform.GetComponentInChildren<NormalShootManager>().OnChangeWeapon += weapon.ChangeWeaponUI; //ToDo:Here 武器の変更ごとに発火されるActionがいる
    }

}
