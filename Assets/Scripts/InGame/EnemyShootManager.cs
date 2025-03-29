using SymphonyFrameWork.CoreSystem;
using UnityEngine;

public class EnemyShootManager : MonoBehaviour, PauseManager.IPausable
{//TODO:HERE NormalShootと統合する
    [Header("発射間隔")]
    [SerializeField]
    private float _interval = 1;

    [Header("飛ばす弾丸の設定")]
    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private float _bulletSpeed = 3;
    [Tooltip("弾丸が飛び続ける秒数")]
    [SerializeField]
    private float _bulletDuration = 2;

    private PlayerController _playerController;
    private EnemyManager _enemyManager;
    private float _timer = 0;

    private bool _isPause;
    void Start()
    {
        _playerController = SingletonDirector.GetSingleton<PlayerController>();
        _enemyManager = GetComponent<EnemyManager>();

        PauseManager.IPausable.RegisterPauseManager(this);
    }

    void Update()
    {
        if (_isPause)
        {
            _timer += Time.deltaTime;
        }

        else if (_timer + _interval < Time.time)
        {
            _timer = Time.time;
            Shoot();
        }
    }

    private async void Shoot()
    {
        if (_playerController)
        {
            Vector2 direction = (_playerController.transform.position - _enemyManager.transform.position).normalized;
            AsyncInstantiateOperation operation = InstantiateAsync(_bullet, transform.position, Quaternion.Euler(direction));
            await operation;
            foreach (GameObject obj in operation.Result)
            {

                if (!obj.TryGetComponent<BulletManager>(out var mg))
                    mg = obj.AddComponent<BulletManager>();

                mg.SetStatus(_enemyManager.Attack);

                if (obj.TryGetComponent<Rigidbody2D>(out var rb))
                {
                    rb.gravityScale = 0;
                    rb.linearDamping = 0;
                    rb.linearVelocity = direction * _bulletSpeed;
                }
                if (obj.TryGetComponent<CircleCollider2D>(out var cc))
                {
                    cc.isTrigger = true;
                }
                await PauseManager.PausableWaitForSecondAsync(_bulletDuration);
                Destroy(obj);
            }
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

    [RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
    public class BulletManager : MonoBehaviour, PauseManager.IPausable
    {
        private float _attack;
        Rigidbody2D _rb;
        public void SetStatus(float attack)
        {
            _attack = attack;
            _rb = GetComponent<Rigidbody2D>();
            PauseManager.IPausable.RegisterPauseManager(this);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<PlayerController>(out var player))
            {
                player.AddDamage(_attack);
                Destroy(gameObject);
            }
        }
        private void OnDestroy()
        {
            PauseManager.IPausable.RemovePauseManager(this);
        }
        public void Pause()
        {
            if( _rb != null ) 
                _rb.simulated = false;
            Debug.Log(_rb.ToString());
        }

        public void Resume()
        {
            if (_rb != null)
                _rb.simulated = true;
        }
    }
}
