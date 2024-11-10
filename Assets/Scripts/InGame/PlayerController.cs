using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MobBase
{
    public static PlayerController player;

    private Rigidbody2D _rigidbody;

    protected override void Awake_S()
    {
        player = this;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Initialize()
    {
        _currentHealth = _maxHealth;
    }

    private void Update()
    {
        Vector2 dir = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _rigidbody.linearVelocity = dir * _agility / 5;
    }

    protected override void DeathBehaviour()
    {
        Debug.Log("death");
    }
}
