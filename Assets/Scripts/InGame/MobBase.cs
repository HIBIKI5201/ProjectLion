using UnityEngine;
using UnityEngine.U2D.Animation;

public class MobBase<MobDataKind> : MonoBehaviour where MobDataKind : MobData_S
{
    [SerializeField]
    private MobDataKind _data;
    public MobDataKind BaseData { get => _data; }
    public MobData Data { get => _data.Data; }

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

    protected SpriteResolver _spriteResolver;
    public float Attack { get => _attack; }
    private void Awake()
    {
        if (_data != null)
        {
            LoadData(_data.Data);
        }
        else Debug.LogWarning($"{gameObject.name}Ç…ÉfÅ[É^Ç™Ç†ÇËÇ‹ÇπÇÒ");

        Awake_S();
    }

    protected virtual void Awake_S() { }

    public virtual void LoadData(global::MobData data)
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

    protected void ChangeSprite(string category, string label)
    {
        _spriteResolver.SetCategoryAndLabel(category, label);

        _spriteResolver.ResolveSpriteToSpriteRenderer();
    }
}
