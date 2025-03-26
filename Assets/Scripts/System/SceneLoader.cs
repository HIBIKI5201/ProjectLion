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
        Scene activeScene = SceneManager.GetActiveScene();//現在のmainSceneを取得
        var task = SceneManager.LoadSceneAsync(_sceneNames[kind], LoadSceneMode.Additive);//Sceneをロード
        
        Debug.Log($"Active Scene: {SceneManager.GetActiveScene().name}");
        //ロード待機
        task.allowSceneActivation = false;//シーンを一時的に無効化
        while (task.progress < 0.9f)
        {
            Debug.Log(task.progress);
            await Task.Yield();
        }
        Debug.Log($"Active Scene: {SceneManager.GetActiveScene().name}");
        Scene newScene = SceneManager.GetSceneByName(_sceneNames[kind]);
        Debug.Log($"Loading Scene: {newScene.name}");
        
        task.allowSceneActivation = true;//シーンを一時的に無効化
        await task;
        //アクティブなシーンの変更
        Debug.Log($"Loading Scene: {newScene.name} Unload:{activeScene.name}");
        Debug.Log($"Active Scene: {SceneManager.GetActiveScene().name}");
        SceneManager.SetActiveScene(newScene);
        //前回のシーンの解放
        var unlodeTask = SceneManager.UnloadSceneAsync(activeScene);

        _currentScene = activeScene;
    }
    
    public static async Task LoadSceneAsync(string str)
    {
        Scene activeScene = SceneManager.GetActiveScene();//現在のmainSceneを取得
        var task = SceneManager.LoadSceneAsync(str, LoadSceneMode.Additive);//Sceneをロード
        
        Debug.Log($"Active Scene: {SceneManager.GetActiveScene().name}");
        //ロード待機
        task.allowSceneActivation = false;//シーンを一時的に無効化
        while (task.progress < 0.9f)
        {
            Debug.Log(task.progress);
            await Task.Yield();
        }
        Debug.Log($"Active Scene: {SceneManager.GetActiveScene().name}");
        Scene newScene = SceneManager.GetSceneByName(str);
        Debug.Log($"Loading Scene: {newScene.name}");
        
        task.allowSceneActivation = true;//シーンを一時的に無効化
        await task;
        //アクティブなシーンの変更
        Debug.Log($"Loading Scene: {newScene.name} Unload:{activeScene.name}");
        Debug.Log($"Active Scene: {SceneManager.GetActiveScene().name}");
        SceneManager.SetActiveScene(newScene);
        //前回のシーンの解放
        var unlodeTask = SceneManager.UnloadSceneAsync(activeScene);

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