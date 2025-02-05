using UnityEngine;
using SymphonyFrameWork.CoreSystem;

public class EnemyShootManager : MonoBehaviour
{
    private PlayerController controller;

    [SerializeField]
    private float _interval = 5;

    [Header("îÚÇŒÇ∑íeä€ÇÃê›íË")]
    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private float _bulletSpeed = 3;
    [SerializeField]
    private float _bulletDuration = 2;

    private float _timer = 0;

    void Start()
    {
        controller = SingletonDirector.GetSingleton<PlayerController>();
    }

    void Update()
    {
        if (_timer + _interval < Time.time)
        {
            _timer = Time.time;
            Shoot();
        }
    }

    private async void Shoot()
    {
        
    }
}
