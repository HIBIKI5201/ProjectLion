using JetBrains.Annotations;
using System;
using UnityEngine;

public class SpecialAttackManager : MonoBehaviour
{
    private float _specialExperiancePoint = 0;
    public float SpecialExperiancePoint { get => _specialExperiancePoint; }
    [SerializeField]
    private float _specialRequirePoint = 500;
    public float SpecialRequirePoint { get => _specialRequirePoint; }

    private bool _specialReady = false;//���̕ϐ���true�ɂȂ�����K�E�Z���g����



    //�K�E�Z���g���邩�ǂ���
    public void AddSpecialExperiance(float point)
    {
        _specialExperiancePoint += point;
        if (!_specialReady && _specialRequirePoint <= _specialExperiancePoint)
        {
            Debug.Log("Special Ready");
            _specialReady = true;
        }
    }

    public event Action OnSpecialbutton;
    private void SpecialAttack()
    {
        OnSpecialbutton?.Invoke();
        _specialReady = false;
        _specialExperiancePoint = 0;
    }
}
