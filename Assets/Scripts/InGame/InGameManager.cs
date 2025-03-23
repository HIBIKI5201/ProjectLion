using SymphonyFrameWork.CoreSystem;
using System;
using UnityEngine;

public class InGameManager : MonoBehaviour, PauseManager.IPausable
{
    private float _time;
    bool _isPause;
    public int EnemyKillCount;

    public Action<float> OnTimerChanged;
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        SingletonDirector.GetSingleton<PlayerController>()?.Initialize();
        FindAnyObjectByType<EnemyGenerator>()?.Initialize();
    }

    private void Update()
    {
        if (!_isPause)
        {
            _time += Time.deltaTime;
            OnTimerChanged?.Invoke(_time);
        }

    }

    void PauseManager.IPausable.Pause()
    {
        _isPause = true;
    }

    void PauseManager.IPausable.Resume()
    {
        _isPause = false;
    }
}

[Serializable]
public class PlayerSaveData
{
    public string PlayerName;
    public string TestString;
    public int TestInt;
    public PlayerSaveData()
    {
        PlayerName = "new player";
        TestString = "test string kun";
        TestInt = 100;
    }
}