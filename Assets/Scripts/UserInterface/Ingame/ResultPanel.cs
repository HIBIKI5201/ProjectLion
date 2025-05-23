using System.Threading.Tasks;
using SymphonyFrameWork.CoreSystem;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class ResultPanel : UIElement_B
{
    [UxmlAttribute] private SceneKind loadScene;
    [UxmlAttribute] private float _waitResultPanel;

    [UxmlAttribute] private string _enemyCountText = "倒した敵の数：";

    private InGameManager _inGameManager;
    private Label EnemyKillCount;

    public ResultPanel() : base("UITK_Items/UXML/InGame/ResultPanel")
    {
    }

    protected override Task Initialize_S(TemplateContainer container)
    {
        EnemyKillCount = container.Q<Label>("EnemyKillCount");
        var ResetButton = container.Q<Button>("RestartButton");

        ResetButton.clickable = new Clickable(() =>
        {
            PauseManager.Pause = false;
            Debug.Log("Restart");
            //SceneLoader.LoadSceneAsync(loadScene);
            GameBaseSystem.instance.ChangeScene(loadScene);
        });
        return Task.CompletedTask;
    }

    public async Task ActivateResultPanel(int enmeyKillCount)
    {
        PauseManager.Pause = true;
        await Awaitable.WaitForSecondsAsync(_waitResultPanel);
        EnemyKillCount.text　= _enemyCountText + enmeyKillCount.ToString();
        style.visibility = Visibility.Visible;
    }
}