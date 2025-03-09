using SymphonyFrameWork.CoreSystem;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.U2D.Animation;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyManager : MobBase<EnemyData>
{
    private Rigidbody2D _rigidBody;

    private event Action DeathAction;

    private SpriteRenderer _spriteRenderer;

    private int _repopRange = 20;

    private Coroutine _attackCoroutine;

    private GameObject _player;
    private CircleCollider2D _playerCollider;

    [SerializeField]
    private GameObject _experianceObj;

    protected override void Awake_S()
    {
        _player = SingletonDirector.GetSingleton<PlayerController>().gameObject;
        _playerCollider = _player.GetComponent<CircleCollider2D>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _spriteResolver = _spriteRenderer.GetComponent<SpriteResolver>();
    }

    private void Update()
    {
        float distance = Vector2.Distance(_player.transform.position, transform.position);
        if (distance <= _repopRange)
        {
            if (distance > _playerCollider.radius)
            {
                Vector2 direction = (_player.transform.position - transform.position).normalized;
                _rigidBody.linearVelocity = direction * Agility / 5;

                ChangeSprite(direction.x >= 0 ? "Right" : "Left", BaseData.Data.Name);
            }
        }
        else
        {
            Repop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player))
            if (gameObject.activeInHierarchy)
                _attackCoroutine = StartCoroutine(PlayerAttack.Attack(player, Attack));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out _) && _attackCoroutine is not null)
            StopCoroutine(_attackCoroutine);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="action">死亡時に呼び出されるAction</param>
    /// <param name="statusMultiplier">Enemyのステータス倍率</param>
    public void Init(Action action, float statusMultiplier)
    {
        _currentHealth = MaxHealth;
        DeathAction = action;
        _repopRange = 30;

        foreach (BuffKind buffKind in Enum.GetValues(typeof(BuffKind)))
        {
            Setbuff(buffKind, statusMultiplier);
        }
        //Repop();//SetPosに変更
        gameObject.SetActive(true);
    }

    public void SetPos(float angle, float radius)
    {
        angle = angle * Mathf.Deg2Rad;
        _rigidBody.AddForce(new Vector2(Random.Range(-1,1),Random.Range(-1,1)));//重なってスポーンすることがあったので少しずらす
        transform.position = _player.transform.position +
                             new Vector3(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle));
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
        //�o���l���h���b�v
        Instantiate(_experianceObj, transform.position, Quaternion.identity).
            GetComponent<Experiance>().Initialize(base.BaseData.DropExperience);//�o���l��������
    }

    protected override async void HitDamageBehaviour()
    {
        base.HitDamageBehaviour();

        _spriteRenderer.color = Color.red;
        await Awaitable.WaitForSecondsAsync(0.2f);
        _spriteRenderer.color = Color.white;
    }

    public override void Pause()
    {
        _rigidBody.simulated = false;
    }
    public override void Resume()
    {
        _rigidBody.simulated = true;
    }
}

public static class PlayerAttack
{
    public static IEnumerator Attack(PlayerController player, float damage)
    {
        while (true)
        {
            player.AddDamage(damage);
            yield return PauseManager.PausableWaitForSecond(1);
        }
    }
}