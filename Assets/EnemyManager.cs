using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    const float _moveSpeed = 1;

    void Update()
    {
        transform.position += (PlayerController.PlayerObject.transform.position - transform.position).normalized * _moveSpeed * Time.deltaTime;
    }
}
