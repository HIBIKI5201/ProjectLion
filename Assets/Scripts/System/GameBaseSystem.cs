using System;
using System.Collections;
using System.Threading.Tasks;
using SymphonyFrameWork.CoreSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBaseSystem : MonoBehaviour
{
    public static GameBaseSystem instance;
    public static Action OnReset;

    private AudioManager _audioManager;
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            Scene scene = SceneManager.CreateScene("SystemScene");
            SceneManager.MoveGameObjectToScene(gameObject, scene);
        }

        _audioManager = GetComponentInChildren<AudioManager>();
    }

    public async Task ChangeScene(SceneKind kind)
    {
        await FadeSystem.Instance.Fade(FadeSystem.FadeMode.FadeOut);
        LoadReset();
        await SceneLoader.LoadSceneAsync(kind);
        //PauseManager.Pause = false;
        await FadeSystem.Instance.Fade(FadeSystem.FadeMode.FadeIn);
    }    
    public async Task ChangeScene(string str)
    {

        await FadeSystem.Instance.Fade(FadeSystem.FadeMode.FadeOut);
        LoadReset();
        await SceneLoader.LoadSceneAsync(str);
        PauseManager.Pause = false;
        await FadeSystem.Instance.Fade(FadeSystem.FadeMode.FadeIn);
    }
    public void LoadReset()
    {
        if (SingletonDirector.GetSingleton<PlayerController>())
        {
            SingletonDirector.DestroySingleton<PlayerController>();
        }
        OnReset?.Invoke();
    }
}