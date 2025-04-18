using SymphonyFrameWork.CoreSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class MobBase<MobDataKind> : MonoBehaviour, PauseManager.IPausable, IHaveMobData where MobDataKind : MobData_S
{
    [SerializeField]
    private MobDataKind _baseData;
    [SerializeField]
    private MobData _data;
    public MobDataKind BaseData { get => _baseData; }
    public MobData Data { get => _data; }

    protected float _currentHealth;

    protected SpriteResolver _spriteResolver;

    Dictionary<BuffKind, float> _buffs = new Dictionary<BuffKind, float>()
    {
        {BuffKind.HPBuff, 1},
        {BuffKind.AgilityBuff, 1},
        {BuffKind.AttackCoolTimeBuff, 1},
        {BuffKind.AttackRangeBuff, 1},
        {BuffKind.AttackPowerBuff, 1},
    };

    public float CurrentHealth { get => _currentHealth; }
    public float MaxHealth { get => _data.MaxHealth * _buffs[BuffKind.HPBuff]; }
    public float Attack { get => _data.Attack * _buffs[BuffKind.AttackPowerBuff]; }
    public float AttackCoolTime { get => _data.AttackCoolTime * _buffs[BuffKind.AttackCoolTimeBuff]; }
    public float AttackRange { get => _data.AttackRange * _buffs[BuffKind.AttackRangeBuff]; }
    public float Agility { get => _data.Agility * _buffs[BuffKind.AgilityBuff]; }
    private void Awake()
    {
        if (_baseData != null)
        {
            LoadData(_baseData.Data);
        }
        else Debug.LogWarning($"{gameObject.name}にデータがありません");

        PauseManager.IPausable.RegisterPauseManager(this);

        Awake_S();
    }
    private void OnDestroy()
    {
        PauseManager.IPausable.RemovePauseManager(this);
    }

    protected virtual void Awake_S() { }

    public virtual void LoadData(global::MobData data)
    {
        _data = data;
    }

    public virtual void AddDamage(float damage)
    {
        _currentHealth -= Mathf.Max(0, damage);
        HitDamageBehaviour();
        //Debug.Log($"現在のHPは　{_currentHealth}");

        if (_currentHealth <= 0)
        {
            DeathBehaviour();
        }
    }

    public void AddHealth(float health)
    {
        if (_currentHealth + health >= MaxHealth)
        {
            _currentHealth = MaxHealth;
            HealBehaviour();
            return;
        }

        _currentHealth += Mathf.Max(0, health);
        HealBehaviour();
        _currentHealth = Mathf.Min(_currentHealth, MaxHealth);
    }

    public void Setbuff(BuffKind kind, float latio)
    {
        _buffs[kind] = latio;
    }

    protected virtual void HitDamageBehaviour() { }
    protected virtual void HealBehaviour() { }
    protected virtual void DeathBehaviour() { }

    protected void ChangeSprite(string category, string label)
    {
        _spriteResolver.SetCategoryAndLabel(category, label);

        _spriteResolver.ResolveSpriteToSpriteRenderer();
    }


    public virtual void Pause()
    {
        throw new System.NotImplementedException();
    }

    public virtual void Resume()
    {
        throw new System.NotImplementedException();
    }

}
public enum BuffKind
{
    HPBuff,
    AttackPowerBuff,
    AttackCoolTimeBuff,
    AttackRangeBuff,
    AgilityBuff,
}
