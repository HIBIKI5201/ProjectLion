using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static Dictionary<SceneKind, string> _sceneNames = new()
    {
        {SceneKind.Title, "" },
        {SceneKind.Home, "" },
        {SceneKind.Ingame, "" },
    };

    private static Scene _currentScene;

    public static async Task LoadSceneAsync(SceneKind kind)
    {
        Scene activeScene = SceneManager.GetSceneByName(_sceneNames[kind]);
        var task = SceneManager.LoadSceneAsync(activeScene.name);
        await task;
        SceneManager.SetActiveScene(activeScene);
        
        task = SceneManager.UnloadSceneAsync(_currentScene.name);
        await task;
        _currentScene = activeScene;
    }
}

public enum SceneKind
{
    Title,
    Home,
    Ingame,
}