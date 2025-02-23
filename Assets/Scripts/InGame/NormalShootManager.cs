using SymphonyFrameWork.CoreSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class NormalShootManager : MonoBehaviour
{
    private MobBase<MobData_S> controller;

    private List<EnemyManager> enemies = new();
    private float _interval = 1;
    private float _timer = 0;
    private AudioManager _audioManager;
    private Vector3 _range;

    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private float _bulletSpeed = 3;
    [SerializeField]
    private float _bulletDuration = 2;


    private void Start()
    {
        controller = transform.parent.GetComponent<MobBase<MobData_S>>();
        _interval = controller.AttackSpeed;
        _range = GetComponent<Transform>().localScale;
        GetComponent<Transform>().localScale = _range = new Vector3(controller.AttackRange, 0f, 0f);
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
            GetComponent<Transform>().localScale = _range;
            Debug.Log($"ƒŒƒ“ƒW‚Í {_range}");
        }

        if (_timer + _interval < Time.time && enemies.Count > 0)
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
                Destroy(obj, _bulletDuration);

                if (!obj.TryGetComponent<BulletManager>(out var mg))
                    mg = obj.AddComponent<BulletManager>();

                mg.SetStatus(controller.Attack);

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

    [RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
    public class BulletManager : MonoBehaviour
    {
        private float _damage = 1;
        public void SetStatus(float damage)
        {
            _damage = damage;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                if (collision.gameObject.TryGetComponent(out EnemyManager enemy))
                {
                    enemy.AddDamage(_damage);
                    Destroy(gameObject);
                }
            }
        }
    }
}
