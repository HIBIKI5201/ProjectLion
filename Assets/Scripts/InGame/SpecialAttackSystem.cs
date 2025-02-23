using SymphonyFrameWork.CoreSystem;
using UnityEngine;

public class SpecialAttackSystem : MonoBehaviour
{
    [SerializeField]
    private float _damageRate = 100;
    private float _attack;

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
        _spriteRenderer.enabled = false;
        _boxCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.TryGetComponent(out EnemyManager enemy))
            {
                enemy.AddDamage(_damageRate * _attack);
            }
        }
    }
}
