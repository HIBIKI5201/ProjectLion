using SymphonyFrameWork.CoreSystem;
using System;
using UnityEngine;

public class SpecialAttackManager : MonoBehaviour
{
    //���������p�ϐ�
    private float _specialExperiancePoint = 0;
    public float SpecialExperiancePoint { get => _specialExperiancePoint; }
    [SerializeField]
    private float _specialRequirePoint = 500;
    public float SpecialRequirePoint { get => _specialRequirePoint; }

    //�������e�ϐ�
    private bool _specialReady = false;//���̕ϐ���true�ɂȂ�����K�E�Z���g����
    public event Action<float> SpecialEvant;

    [SerializeField]
    private GameObject _specialObj;
    //[SerializeField]
    //private float _specialRate = 3;
    [SerializeField]
    private float _specialTime = 3;


    //�o���l�ǉ������A�K�E�Z�������菈��
    public void AddSpecialExperiance(float point)
    {
        _specialExperiancePoint += point;
        SpecialEvant?.Invoke(_specialRequirePoint - _specialExperiancePoint);
        if (!_specialReady && _specialRequirePoint <= _specialExperiancePoint)
        {
            _specialReady = true;
        }
    }
    //�K�E�Z��������
    public async void SpecialAttack(Transform transform, float attack)//�K�E�Z�̃I�u�W�F�N�g���ɕ����č���Ă���֌W�ł��܂������Ȃ���������flort�֐��͎g�p���Ă��܂���
    {
        //�K�E�Z�����̏���
        if (_specialReady)
        {
            _specialObj.SetActive(true);
            await PauseManager.PausableWaitForSecondAsync(_specialTime);
            _specialObj.SetActive(false);
            //�K�E�Z�����I���̏���
            _specialReady = false;
            SpecialEvant?.Invoke(0);
            _specialExperiancePoint = 0;
        }
    }
}
