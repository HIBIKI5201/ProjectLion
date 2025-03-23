using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class ResultPanel : UIElement_B
{
    [UxmlAttribute] private SceneKind loadScene;

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
            Debug.Log("Restart");
            //SceneLoader.LoadSceneAsync(loadScene);
            GameBaseSystem.instance.ChangeScene(loadScene);
        });
        return Task.CompletedTask;
    }

    public Task ActivateResultPanel(int enmeyKillCount)
    {
        EnemyKillCount.text　= _enemyCountText + enmeyKillCount.ToString();
        style.visibility = Visibility.Visible;
        return Task.CompletedTask;
    }
}