using SymphonyFrameWork.CoreSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class NormalShootManager : MonoBehaviour, PauseManager.IPausable
{
    private MobBase<MobData_S> controller;
    private AudioManager _audioManager;

    private WeaponData _nowWeapon;
    public WeaponData NowWeapon { get => _nowWeapon; } 
    public event Action<WeaponData> OnChangeWeapon;

    private List<EnemyManager> enemies = new();
    private float _timer = 0;

    //private float _bulletSpeed = 3;
    //private float _bulletDuration = 2;
    //private float _attackPowerMultiplier = 1;
    private Vector3 _attackRange;//保留
    private float _attackCoolTime = 1;//保留


    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private WeaponData_S[] _weaponData;

    bool _isPause;

    private void Start()
    {
        controller = transform.parent.GetComponent<MobBase<MobData_S>>();
        Init();
        PauseManager.IPausable.RegisterPauseManager(this);
    }

    private void Init()
    {
        _nowWeapon = _weaponData[0].Data;
        _attackCoolTime = _nowWeapon.AttackCoolTimeModifier;
        GetComponent<Transform>().localScale = _attackRange = new Vector3(_nowWeapon.AttackRangeMultiplier, 0f, 0f);
    }

    private void Update()
    {
        if (_attackCoolTime != controller.AttackCoolTime / _nowWeapon.AttackCoolTimeModifier)
        {
            _attackCoolTime = controller.AttackCoolTime / _nowWeapon.AttackCoolTimeModifier;
        }

        if (_attackRange.x != controller.AttackRange * _nowWeapon.AttackRangeMultiplier)
        {
            _attackRange = new Vector3(_nowWeapon.AttackRangeMultiplier, 0f, 0f);
            transform.localScale = _attackRange;
            //Debug.Log($"�����W�� {_attackRange}");
        }

        if (_isPause)
        {
            _timer += Time.deltaTime;
        }
        else if (_timer + _attackCoolTime < Time.time && enemies.Count > 0)
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
            AsyncInstantiateOperation operation = InstantiateAsync(_bullet, controller.transform.position, Quaternion.Euler(direction));
            await operation;
            foreach (GameObject obj in operation.Result)
            {
                Destroy(obj, _nowWeapon.BulletDuration);

                if (!obj.TryGetComponent<BulletManager>(out var mg))
                    mg = obj.AddComponent<BulletManager>();

                mg.SetStatus(controller.Attack, _nowWeapon.AttackPowerMultiplier);

                if (obj.TryGetComponent<Rigidbody2D>(out var rb))
                {
                    rb.gravityScale = 0;
                    rb.linearDamping = 0;
                    rb.linearVelocity = direction * _nowWeapon.BulletSpeed;
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
    /// <summary>
    /// 武器の変更の際に呼び出す処理、引数には武器の番号を指定する
    /// </summary>
    /// <param name="num"></param>
    public void SetWeapon(int num)
    {
        _nowWeapon = _weaponData[(num)].Data;
        OnChangeWeapon?.Invoke(_nowWeapon);
        _attackCoolTime = _nowWeapon.AttackCoolTimeModifier;
        GetComponent<Transform>().localScale = _attackRange = new Vector3(_nowWeapon.AttackRangeMultiplier, 0f, 0f);
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
