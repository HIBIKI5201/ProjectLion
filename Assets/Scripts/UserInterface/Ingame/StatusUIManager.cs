using System.Threading.Tasks;
using Unity.Entities.UniversalDelegates;
using UnityEngine;
using UnityEngine.UIElements;

public partial class StatusUIManager : UIElement_B
{
    public StatusUIManager() : base("UXML/Status.uxml") { }

    StatusUI _attackPower;
    StatusUI _attackSpeed;
    StatusUI _attackRange;
    StatusUI _criticalChance;
    StatusUI _moveSpeed;
    StatusUI _specialAttackPower;
    protected override Task Initialize_S(TemplateContainer container)
    {
        _attackPower = container.Q<StatusUI>("AttackPower");
        _attackSpeed = container.Q<StatusUI>("AttackSpeed");
        _attackRange = container.Q<StatusUI>("AttackRange");
        _criticalChance = container.Q<StatusUI>("CriticalChance");
        _moveSpeed = container.Q<StatusUI>("MoveSpeed");
        _specialAttackPower = container.Q<StatusUI>("SpecialAttackPower");
        return Task.CompletedTask;
    }

    public void OnStatusChange(float attackPower,float attackSpeed,float range,float critical,float moveSpeed,float specialAttack)
    {
            _attackPower.OnStatusChanged(attackPower);
            _attackSpeed.OnStatusChanged(attackSpeed);
            _attackRange.OnStatusChanged(range);
            _criticalChance.OnStatusChanged(critical);
            _moveSpeed.OnStatusChanged(moveSpeed);
            _specialAttackPower.OnStatusChanged(specialAttack);
    }
}
