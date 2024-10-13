using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static GameObject PlayerObject;

    [SerializeField]
    private float _moveSpeed = 1;

    private void Start()
    {
        PlayerObject = gameObject;
    }

    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        transform.position += (Vector3)input * _moveSpeed * Time.deltaTime;
    }
}
