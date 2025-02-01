using UnityEngine;

public class SpecialAttackSystem : MonoBehaviour
{
    [SerializeField]
    private float _damageRate = 100;
    private float _attack;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.TryGetComponent(out EnemyManager enemy))
            {
                _attack = FindAnyObjectByType<PlayerController>().Attack;//よくない実装、必殺技のオブジェクトを二つ用意している弊害で必殺技発動のたびに二度findしている
                enemy.AddDamage(_damageRate * _attack);
            }
        }
    }
}
