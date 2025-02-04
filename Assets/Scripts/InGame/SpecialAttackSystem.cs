using SymphonyFrameWork.CoreSystem;
using UnityEngine;

public class SpecialAttackSystem : MonoBehaviour
{
    [SerializeField]
    private float _damageRate = 100;
    private float _attack;

    private void Start()
    {
        _attack = SingletonDirector.GetSingleton<PlayerController>().Attack;//���߂ăV���O���g���֘A���g�����̂Ŏg�����Ԉ���Ă邩��
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
