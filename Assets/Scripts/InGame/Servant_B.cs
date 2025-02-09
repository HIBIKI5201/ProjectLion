using SymphonyFrameWork.CoreSystem;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Servant_B : MobBase<MobData_S>
{
    [SerializeField] float _moveStopDistanse;
    [SerializeField] float _moveScale;

    protected PlayerController _player;
    Rigidbody2D _rb;
    bool _isPause;

    void Start()
    {
        _player = SingletonDirector.GetSingleton<PlayerController>();
        _rb = GetComponent<Rigidbody2D>();
        Start_S();
        var n = FindAnyObjectByType<LevelUpManager>();
        n.OnLevelChange += x => LevelUp(x);
    }
    protected virtual void Start_S() { }

    // Update is called once per frame
    void Update()
    {
        if(_isPause) return;

        Vector2 n = _player.transform.position - this.transform.position;
        if (n.sqrMagnitude > _moveStopDistanse * _moveStopDistanse)
        {
            _rb.linearVelocity = n * _moveScale;
        }

        Update_S();
    }
    protected virtual void Update_S() { }
    protected virtual void LevelUp(Dictionary<ItemKind, int> haveItem)
    {
        LoadData(new MobData(Data,
                    health: 1.1f * Data.MaxHealth,
                    attack: 1.1f * Data.Attack,
                    defense: 0,//ItemHaveValue[ItemKind.DefenseUp] * 0.1f * player.Data.Defense,
                    agility: 1.1f * Data.Agility,
                    attackRange: 1.1f * Data.AttackRange,
                    attackSpeed: 1.1f * Data.AttackSpeed));
    }

    public abstract void Skill();

    public override void Pause()
    {
        _rb.linearVelocity = Vector2.zero;
        _rb.useFullKinematicContacts = true;
        _isPause = true;
    }
    public override void Resume()
    {
        _rb.useFullKinematicContacts = false;
        _isPause = false;
    }
}
