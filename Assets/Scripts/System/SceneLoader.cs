using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private readonly static Dictionary<SceneKind, string> _sceneNames = new()
    {
        {SceneKind.Title, "Title" },
        {SceneKind.Home, "" },
        {SceneKind.Ingame, "SampleScene" },
        {SceneKind.Result, "ResultScene" }
    };

    private static Scene _currentScene;

    public static async Task LoadSceneAsync(SceneKind kind)
    {
        Scene activeScene = SceneManager.GetSceneByName(_sceneNames[kind]);

        if (activeScene.isLoaded)
        {
            var ta = SceneManager.UnloadSceneAsync(activeScene);
        }
        
        var task = SceneManager.LoadSceneAsync(activeScene.name, LoadSceneMode.Additive);
        task.allowSceneActivation = false;
        Debug.Log($"Loading Scene: {activeScene.name}");
        while (!task.isDone)
        {
            if (task.progress >= 0.9f)
            {
                task.allowSceneActivation = true; // シーンをアクティブにする
            }
            await Task.Yield();
        }
        activeScene = SceneManager.GetSceneByName(_sceneNames[kind]);
        SceneManager.SetActiveScene(activeScene);
        if (_currentScene.name is not null)
        {
            task = SceneManager.UnloadSceneAsync(_currentScene.name);
            await task;
        }

        _currentScene = activeScene;
    }
}

public enum SceneKind
{
    Title,
    Home,
    Ingame,
    Result
}