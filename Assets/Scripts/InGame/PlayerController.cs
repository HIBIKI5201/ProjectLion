using SymphonyFrameWork.CoreSystem;
using System;
using UnityEngine;
using UnityEngine.U2D.Animation;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MobBase<MobData_S>
{
    private Rigidbody2D _rigidbody;

    private SpriteRenderer _spriteRenderer;

    public event Action<MobData> OnChangeState;
    public event Action<float> OnDamage;
    public event Action OnDeath;
    protected override void Awake_S()
    {
        SingletonDirector.SetSinglton(this);
        _rigidbody = GetComponent<Rigidbody2D>();
        //一番目のオブジェクトがPlayerの見た目
        _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _spriteResolver = _spriteRenderer.GetComponent<SpriteResolver>();
    }

    public void Initialize()
    {
        _currentHealth = MaxHealth;
    }

    private void Update()
    {
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        _rigidbody.linearVelocity = direction * Agility / 5;
        ChangeSprite(direction.x >= 0 ? "Right" : "Left", "Player");
    }
    public override void LoadData(MobData data)
    {
        base.LoadData(data);
        OnChangeState?.Invoke(data);
    }

    protected override void DeathBehaviour()
    {
        _spriteRenderer.enabled = false;
        OnDeath?.Invoke();
        enabled = false;
        _rigidbody.simulated = false;
    }

    protected override async void HitDamageBehaviour()
    {
        base.HitDamageBehaviour();
        OnDamage?.Invoke(CurrentHealth);
        AudioManager.Instance.PlaySE("SE_Damage");
        _spriteRenderer.color = Color.red;
        await Awaitable.WaitForSecondsAsync(0.2f);
        _spriteRenderer.color = Color.white;
    }

    protected override async void HealBehaviour()
    {
        base.HealBehaviour();
        OnDamage?.Invoke(CurrentHealth);
        //AudioManager.Instance.PlaySE("SE_Heal");現在SEなし
        _spriteRenderer.color = Color.green;
        await Awaitable.WaitForSecondsAsync(0.2f);
        _spriteRenderer.color = Color.white;
    }

    public override void Pause()
    {
        _rigidbody.simulated = false;
    }
    public override void Resume()
    {
        _rigidbody.simulated = true;
    }

    //public void ChangeWeapon(NowWeaponType weaponType)
    //{

    //}
    //public enum NowWeaponType
    //{
    //    Weapon1,
    //    Weapon2,
    //    Weapon3
    //}
}
