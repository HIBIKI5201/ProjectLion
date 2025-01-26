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
                _attack = FindAnyObjectByType<PlayerController>().Attack;//�悭�Ȃ������A�K�E�Z�̃I�u�W�F�N�g���p�ӂ��Ă��镾�Q�ŕK�E�Z�����̂��тɓ�xfind���Ă���
                enemy.AddDamage(_damageRate * _attack);
            }
        }
    }
}
