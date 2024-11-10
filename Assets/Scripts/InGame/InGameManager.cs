using UnityEngine;

public class InGameManager : MonoBehaviour
{
    private void Start()
    {
        PlayerController.player.Initialize();
        FindAnyObjectByType<EnemyGenerator>().Initialize();
    }
}
