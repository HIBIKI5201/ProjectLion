using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyManager : MobBase
{
    private Rigidbody2D _rigidBody;

    event Action DeathAction;


    private int _repopRange = 20;

    private Coroutine _attackCoroutine;

    private GameObject _player;

    protected override void Awake_S()
    {
        _player = PlayerController.player.gameObject;
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Vector2.Distance(_player.transform.position, transform.position) <= _repopRange)
        {
            Vector2 velocity = (_player.transform.position - transform.position).normalized * _agility / 5;
            _rigidBody.linearVelocity = velocity;
        }
        else
            Repop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player))
        {
            _attackCoroutine = StartCoroutine(Attack(player));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out _))
        {
            StopCoroutine(_attackCoroutine);
        }

    }

    public void Init(Action action)
    {
        _currentHealth = _maxHealth;
        DeathAction = action;
        _repopRange = Random.Range(12, 25);

        Repop();
        gameObject.SetActive(true);
    }

    private void Repop()
    {
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        float radius = Random.Range(10, 15);
        transform.position = _player.transform.position + new Vector3(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle));
    }

    protected override void DeathBehaviour()
    {
        DeathAction?.Invoke();
        gameObject.SetActive(false);
    }

    private IEnumerator Attack(PlayerController player)
    {
        while (true)
        {
            player.AddDamage(_attack);
            yield return new WaitForSeconds(1);
        }
    }
}
