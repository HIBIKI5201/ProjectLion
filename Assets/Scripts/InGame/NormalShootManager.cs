using SymphonyFrameWork.CoreSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class NormalShootManager : MonoBehaviour, PauseManager.IPausable
{
    private MobBase<MobData_S> controller;

    private List<EnemyManager> enemies = new();
    private float _interval = 1;
    private float _timer = 0;
    private AudioManager _audioManager;
    private Vector3 _range;
    private GameObject _nowbullet;

    [SerializeField]
    private GameObject[] _bullet;
    [SerializeField]
    private float _bulletSpeed = 3;
    public float BulletSpeed { set => _bulletSpeed = value; }
    [SerializeField]
    private float _bulletDuration = 2;
    public float BulletDuration { set => _bulletDuration = value; }

    [SerializeField] private float _attackPowerMultiplier = 1;
    public float AttackPowerMultiplier { set => _attackPowerMultiplier = value; }

    bool _isPause;

    private void Start()
    {
        controller = transform.parent.GetComponent<MobBase<MobData_S>>();
        _interval = controller.AttackSpeed;
        _nowbullet = _bullet[0];
        _range = GetComponent<Transform>().localScale;
        GetComponent<Transform>().localScale = _range = new Vector3(controller.AttackRange, 0f, 0f);

        PauseManager.IPausable.RegisterPauseManager(this);
    }

    private void Update()
    {
        if (_interval != controller.AttackSpeed)
        {
            _interval = controller.AttackSpeed;
        }

        if (_range.x != controller.AttackRange)
        {
            _range = new Vector3(controller.AttackRange, 0f, 0f);
            transform.localScale = _range;
            Debug.Log($"�����W�� {_range}");
        }

        if (_isPause)
        {
            _timer += Time.deltaTime;
        }
        else if (_timer + _interval < Time.time && enemies.Count > 0)
        {
            _timer = Time.time;
            Shoot();
            AudioManager.Instance.PlaySE("SE_Shoot");
        }
    }

    private async void Shoot()
    {
        EnemyManager target = enemies.OrderBy(e => Vector2.Distance(controller.transform.position, e.transform.position)).First();
        if (target is not null)
        {
            Vector2 direction = (target.transform.position - controller.transform.position).normalized;
            AsyncInstantiateOperation operation = InstantiateAsync(_nowbullet, controller.transform.position, Quaternion.Euler(direction));
            await operation;
            foreach (GameObject obj in operation.Result)
            {
                Destroy(obj, _bulletDuration);

                if (!obj.TryGetComponent<BulletManager>(out var mg))
                    mg = obj.AddComponent<BulletManager>();

                mg.SetStatus(controller.Attack, _attackPowerMultiplier);

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
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.TryGetComponent(out EnemyManager enemy))
            {
                enemies.Add(enemy);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.TryGetComponent(out EnemyManager enemy))
            {
                enemies.Remove(enemy);
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

    public void SetWeapon(int num)
    {
        _nowbullet = _bullet[(num)];
    }

    [RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
    public class BulletManager : MonoBehaviour, PauseManager.IPausable
    {
        private float _damage = 1;
        private float _attackPowerMultiplier;
        Rigidbody2D _rb;

        public void SetStatus(float damage, float attackPowerMultiplier)
        {
            _damage = damage;
            _attackPowerMultiplier = attackPowerMultiplier;
            _rb = GetComponent<Rigidbody2D>();
            PauseManager.IPausable.RegisterPauseManager(this);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                if (collision.gameObject.TryGetComponent(out EnemyManager enemy))
                {
                    enemy.AddDamage(_damage * _attackPowerMultiplier);
                    Destroy(gameObject);
                }
            }
        }
        private void OnDestroy()
        {
            PauseManager.IPausable.RemovePauseManager(this);
        }
        public void Pause()
        {
            _rb.simulated = false;
        }

        public void Resume()
        {
            _rb.simulated = true;
        }
    }
}
