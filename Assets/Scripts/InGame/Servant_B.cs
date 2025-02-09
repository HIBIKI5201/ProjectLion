using SymphonyFrameWork.CoreSystem;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Servant_B : MobBase<MobData_S>
{
    [SerializeField] float _moveStopDistanse;
    [SerializeField] float _moveScale;

    PlayerController _player;
    Rigidbody2D _rb;
    bool _isPause;

    void Start()
    {
        _player = SingletonDirector.GetSingleton<PlayerController>();
        _rb = GetComponent<Rigidbody2D>();
        OnStart();
    }
    protected virtual void OnStart() { }

    // Update is called once per frame
    void Update()
    {
        if(_isPause) return;

        Vector2 n = _player.transform.position - this.transform.position;
        if (n.sqrMagnitude > _moveStopDistanse * _moveStopDistanse)
        {
            _rb.linearVelocity = n * _moveScale;
        }

        OnUpdate();
    }
    protected virtual void OnUpdate() { }

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
