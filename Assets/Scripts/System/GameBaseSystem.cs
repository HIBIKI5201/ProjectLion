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
        if (SingletonDirector.GetSingleton<PlayerController>())
        {
            SingletonDirector.DestroySingleton<PlayerController>();
        }
    
        await FadeSystem.Instance.Fade(FadeSystem.FadeMode.FadeOut);
        PauseManager.Pause = false;
        await SceneLoader.LoadSceneAsync(kind);
        await FadeSystem.Instance.Fade(FadeSystem.FadeMode.FadeIn);
    }    
    public async Task ChangeScene(string str)
    {
        if (SingletonDirector.GetSingleton<PlayerController>())
        {
            SingletonDirector.DestroySingleton<PlayerController>();
        }
        OnReset?.Invoke();

        await FadeSystem.Instance.Fade(FadeSystem.FadeMode.FadeOut);
        await SceneLoader.LoadSceneAsync(str);
        await FadeSystem.Instance.Fade(FadeSystem.FadeMode.FadeIn);
    }
}