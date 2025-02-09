using SymphonyFrameWork.CoreSystem;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class MobBase<MobDataKind> : MonoBehaviour, PauseManager.IPausable where MobDataKind : MobData_S
{
    [SerializeField]
    private MobDataKind _baseData;
    [SerializeField]
    private MobData _data;
    public MobDataKind BaseData { get => _baseData; }
    public MobData Data { get => _data; }

    protected float _currentHealth;

    protected SpriteResolver _spriteResolver;
    public float MaxHealth { get => _data.MaxHealth; }
    public float Attack { get => _data.Attack; }
    public float AttackSpeed { get => _data.AttackSpeed; }
    public float AttackRange { get => _data.AttackRange; }
    public float Agility { get => _data.Agility; }

    int _buffList;
    private void Awake()
    {
        if (_baseData != null)
        {
            LoadData(_baseData.Data);
        }
        else Debug.LogWarning($"{gameObject.name}�Ƀf�[�^������܂���");

        Awake_S();
    }

    protected virtual void Awake_S() { }

    public virtual void LoadData(global::MobData data)
    {
        _data = data;
    }

    public virtual void AddDamage(float damage)
    {
        _currentHealth -= Mathf.Max(damage, 0);
        HitDamageBehaviour();

        if (_currentHealth <= 0)
        {
            DeathBehaviour();
        }
    }
    void Setbuff()
    {

    }

    protected virtual void HitDamageBehaviour() { }
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
