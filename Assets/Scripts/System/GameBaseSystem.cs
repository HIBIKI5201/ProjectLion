using System.Collections;
using SymphonyFrameWork.CoreSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBaseSystem : MonoBehaviour
{
    public static GameBaseSystem instance;

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

    public async void ChangeScene(SceneKind kind)
    {
        if (SingletonDirector.GetSingleton<PlayerController>())
        {
            SingletonDirector.DestroySingleton<PlayerController>();
        }
        await SceneLoader.LoadSceneAsync(kind);
    }
}