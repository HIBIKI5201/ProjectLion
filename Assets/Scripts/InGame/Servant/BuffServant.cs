using UnityEngine;

public class BuffServant : Servant_B
{

    [Space]
    [SerializeField] float _skillTime;
    [SerializeField] float _skillCoolTime;
    [Space]
    [SerializeField] BuffKind _buffKind;
    [SerializeField] float _buffLatio = 1;

    bool _useSkill;
    protected float _timer = 0;

    protected override void Start_S()
    {
    }
    protected override void Update_S()
    {
        _timer += Time.deltaTime;
        if (_timer > _skillCoolTime && !_useSkill)
        {
            _timer = 0;
            _useSkill = true;
            _player.Setbuff(BuffKind.AttackPowerBuff, latio: 1.5f);
            Debug.Log("buff" + _player.Attack.ToString());
        }
        else if (_timer > _skillTime && _useSkill)
        {
            _timer = 0;
            _useSkill = false;
            _player.Setbuff(BuffKind.AttackPowerBuff, latio: 1);
            Debug.Log("buffが切れた" + _player.Attack.ToString());
        }
    }

    public override void Skill()
    {

    }
}
