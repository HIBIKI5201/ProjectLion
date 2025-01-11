using SymphonyFrameWork.CoreSystem;
using System;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    private float _ingameStartTime;

    private void Start()
    {
        SingletonDirector.GetSingleton<PlayerController>().Initialize();
        FindAnyObjectByType<EnemyGenerator>().Initialize();
    }

    private void Update()
    {
        if (Time.time - _ingameStartTime > 10)
        {

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