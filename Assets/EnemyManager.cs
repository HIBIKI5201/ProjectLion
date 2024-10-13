using System;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyManager : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    const float _moveSpeed = 1;

    event Action _deathAction;

    private float _maxHealth = 100;
    private float _currentHealth;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _rigidbody.velocity = (PlayerController.PlayerObject.transform.position - transform.position).normalized * _moveSpeed;
    }

    public void Init(Action action)
    {
        _currentHealth = _maxHealth;
        _deathAction = action;

        transform.position = new Vector2(UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(-10, 10));
        gameObject.SetActive(true);
    }

    public void AddDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            _deathAction?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
