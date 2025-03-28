using SymphonyFrameWork.CoreSystem;
using UnityEngine;
using System.Collections;
using TMPro;

public class SpecialAttackSystem : MonoBehaviour
{
    [SerializeField]
    private float _damageRate = 100;
    private float _attack;

    private Coroutine _attackCoroutine;

    private float _attackbuff;
    public float Attackbuff { set { _attackbuff = value; } }

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    public void Init()
    {
        _spriteRenderer.enabled = true;
        _boxCollider.enabled = true;
        _attack = SingletonDirector.GetSingleton<PlayerController>().Attack;
        _attack += (_attack * _attackbuff * 0.01f);
    }
    public void End()
    {
        _attackCoroutine = null;
        _spriteRenderer.enabled = false;
        _boxCollider.enabled = false;
    }

    private static class SpecialAttack
    {
        public static IEnumerator Attack(EnemyManager enemy, float damage)
        {
            while (true)
            {
                Debug.Log($"必殺技{damage}");
                enemy.AddDamage(damage);
                yield return new WaitForSeconds(1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyManager enemy))
        {
            _attackCoroutine = StartCoroutine(SpecialAttack.Attack(enemy, _attack * _damageRate));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out _))
            StopCoroutine(_attackCoroutine);
    }
}
