using UnityEngine;

public class InGameManager : MonoBehaviour
{
    private float _ingameStartTime;

    private void Start()
    {
        PlayerController.player.Initialize();
        FindAnyObjectByType<EnemyGenerator>().Initialize();
    }

    private void Update()
    {
        if (Time.time - _ingameStartTime > 10)
        {
            
        }
    }
}