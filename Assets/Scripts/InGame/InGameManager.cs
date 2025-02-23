using SymphonyFrameWork.CoreSystem;
using System;
using UnityEngine;

public class InGameManager : MonoBehaviour//, PauseManager.IPausable
{
    private float _time;
    bool _isPause;

    public Action<float> OnTimerChanged;
    private void Start()
    {
        SingletonDirector.GetSingleton<PlayerController>().Initialize();
        FindAnyObjectByType<EnemyGenerator>().Initialize();
    }

    private void Update()
    {

        //ポーズテスト用
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Pause");
            PauseManager.Pause = _isPause = !_isPause;
        }


        if (!_isPause)
        {
            _time += Time.deltaTime;
            OnTimerChanged?.Invoke(_time);
        }

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