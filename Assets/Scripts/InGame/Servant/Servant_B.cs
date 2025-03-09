using SymphonyFrameWork.CoreSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Servant_B : MobBase<MobData_S>
{
    [SerializeField] float _moveStopDistanse;

    protected PlayerController _player;
    protected SpriteRenderer _spriteRenderer;
    Rigidbody2D _rb;

    void Start()
    {
        _player = SingletonDirector.GetSingleton<PlayerController>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _spriteResolver = _spriteRenderer?.gameObject.GetComponent<SpriteResolver>();

        _rb = GetComponent<Rigidbody2D>();
        FindAnyObjectByType<LevelUpManager>().OnLevelChange += x => LevelUp(x);
        Start_S();
    }
    protected virtual void Start_S() { }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();

        void Move()
        {
            Vector2 direction = _player.transform.position - this.transform.position;
            if (direction.sqrMagnitude > _moveStopDistanse * _moveStopDistanse)
            {
                var moveVec = direction - direction.normalized * _moveStopDistanse;
                _rb.linearVelocity = moveVec * Data.Agility * 0.1f;//移動速度調整
            }
            ChangeSprite(direction.x >= 0 ? "Right" : "Left", BaseData.Data.Name);
        }
    }
    protected virtual void Update_S() { }
    protected virtual void LevelUp(Dictionary<ItemKind, int> haveItem)
    {
        LoadData(new MobData(Data,
                    health: 1.1f * BaseData.Data.MaxHealth,
                    attack: 1.1f * BaseData.Data.Attack,
                    defense: 0,//ItemHaveValue[ItemKind.DefenseUp] * 0.1f * player.Data.Defense,
                    agility: BaseData.Data.Agility,
                    attackRange: 1.1f * BaseData.Data.AttackRange,
                    attackSpeed: 1.1f * BaseData.Data.AttackSpeed));
    }
    
    public abstract void Skill();

    public override void Pause()
    {
        _rb.simulated = false;
    }
    public override void Resume()
    {
        _rb.simulated = true;
    }
}
