using UnityEngine;

public class BuffServant : Servant_B<BuffServantData_S>
{

    bool _useSkill;
    protected float _timer = 0;

    protected override void Start_S()
    {
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > BaseData.SkillCoolTime && !_useSkill)
        {
            _timer = 0;
            _useSkill = true;
            _player.Setbuff(BuffKind.AttackPowerBuff, latio: BaseData.BuffLate);
            //Debug.Log("buff" + _player.Attack.ToString());
        }
        else if (_timer > BaseData.SkillTime && _useSkill)
        {
            _timer = 0;
            _useSkill = false;
            _player.Setbuff(BuffKind.AttackPowerBuff, latio: 1);
            //Debug.Log("buffが切れた" + _player.Attack.ToString());
        }
    }

    public override void Skill()
    {
    }
}