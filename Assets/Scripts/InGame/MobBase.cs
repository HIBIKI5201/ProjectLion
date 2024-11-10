using UnityEngine;

public class MobBase : MonoBehaviour
{
    [SerializeField]
    private MobData _data;

    [SerializeField]
    protected float _maxHealth = 100;
    protected float _currentHealth;
    [SerializeField]
    protected float _attack = 10;
    [SerializeField]
    protected float _defense = 10;
    [SerializeField]
    protected float _agility = 10;
    [SerializeField]
    protected float _attackSpeed = 10;
    [SerializeField]
    protected float _attackRange = 10;
    private void Awake()
    {
        if (_data != null)
        {
            LoadData(_data);
        }
        else Debug.LogWarning($"{gameObject.name}�Ƀf�[�^������܂���");

        Awake_S();
    }

    protected virtual void Awake_S() { }

    protected virtual void LoadData(MobData data)
    {
        _maxHealth = data.MaxHealth;
        _attack = data.Attack;
        _defense = data.Defense;
        _agility = data.Agility;
        _attackSpeed = data.AttackSpeed;
        _attackRange = data.AttackRange;
    }

    public virtual void AddDamage(float damage)
    {
        _currentHealth -= Mathf.Max(damage - _defense, 0);
        HitDamageBehaviour();

        if (_currentHealth <= 0)
        {
            DeathBehaviour();
        }
    }

    protected virtual void HitDamageBehaviour() { }
    protected virtual void DeathBehaviour() { }
}