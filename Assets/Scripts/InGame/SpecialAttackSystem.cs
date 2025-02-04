using SymphonyFrameWork.CoreSystem;
using UnityEngine;

public class SpecialAttackSystem : MonoBehaviour
{
    [SerializeField]
    private float _damageRate = 100;
    private float _attack;

    private void Start()
    {
        _attack = SingletonDirector.GetSingleton<PlayerController>().Attack;//初めてシングルトン関連を使ったので使い方間違ってるかも
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
