using SymphonyFrameWork.CoreSystem;
using UnityEngine;
using UnityEngine.U2D.Animation;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MobBase<MobData>
{

    private Rigidbody2D _rigidbody;

    private SpriteRenderer _spriteRenderer;
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
        _currentHealth = _maxHealth;
    }

    private void Update()
    {
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        _rigidbody.linearVelocity = direction * _agility / 5;
        ChangeSprite(direction.x >= 0 ? "Right" : "Left" , "Player");
    }

    protected override void DeathBehaviour()
    {

    }

    protected override async void HitDamageBehaviour()
    {
        base.HitDamageBehaviour();

        _spriteRenderer.color = Color.red;
        await Awaitable.WaitForSecondsAsync(0.2f);
        _spriteRenderer.color = Color.white;
    }
}
