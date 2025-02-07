using UnityEngine;
using SymphonyFrameWork.CoreSystem;

public class EnemyShootManager : MonoBehaviour
{
    [Header("î≠éÀä‘äu")]
    [SerializeField]
    private float _interval = 1;

    [Header("îÚÇŒÇ∑íeä€ÇÃê›íË")]
    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private float _bulletSpeed = 3;
    [Tooltip("íeä€Ç™îÚÇ—ë±ÇØÇÈïbêî")][SerializeField]
    private float _bulletDuration = 2;

    private PlayerController _playerController;
    private EnemyManager _enemyManager;
    private float _timer = 0;

    void Start()
    {
        _playerController = SingletonDirector.GetSingleton<PlayerController>();
        _enemyManager = GetComponent<EnemyManager>();
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
        if (_playerController is not null)
        {
            Vector2 direction = (_playerController.transform.position - _enemyManager.transform.position).normalized;
            AsyncInstantiateOperation operation = InstantiateAsync(_bullet, transform.position, Quaternion.Euler(direction));
            await operation;
            foreach (GameObject obj in operation.Result)
            {
                Destroy(obj, _bulletDuration);

                bool existMG = obj.TryGetComponent<BulletManager>(out var mg);
                if (!existMG)
                    mg = obj.AddComponent<BulletManager>();
                mg.SetStatus(_enemyManager.Attack);
                if (obj.TryGetComponent<Rigidbody2D>(out var rb))
                {
                    rb.gravityScale = 0;
                    rb.linearVelocity = direction * _bulletSpeed;
                }
                if (obj.TryGetComponent<CircleCollider2D>(out var cc))
                {
                    cc.isTrigger = true;
                }
            }
        }
    }
    public class BulletManager : MonoBehaviour
    {
        private float _attack;
        public void SetStatus(float attack)
        {
            _attack = attack;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<PlayerController>(out var player))
            {
                player.AddDamage(_attack);
                Destroy(gameObject);
            }
        }
    }
}
