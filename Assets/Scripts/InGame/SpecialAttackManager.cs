using SymphonyFrameWork.CoreSystem;
using System;
using UnityEngine;

public class SpecialAttackManager : MonoBehaviour
{
    //���������p�ϐ�
    [Header("�K�E�Z�̔�������")]
    [Tooltip("�K�E�Z�ɕK�v�Ȍo���l�̏����l")]
    [SerializeField]
    private float _initRequirePoint = 100;
    public float InitRequirePoint { get => _initRequirePoint; }
    private float _specialRequirePoint;
    public float SpecialRequirePoint
    {
        get => _specialRequirePoint;
        set { _specialRequirePoint = value; }
    }

    private float _specialExperiancePoint = 0;
    public float SpecialExperiancePoint
    {
        get => _specialExperiancePoint;
        set
        {
            _specialExperiancePoint = value;
            SpecialEvant?.Invoke(_specialExperiancePoint, _specialRequirePoint);
        }
    }
    private bool _specialReady = false;//���̕ϐ���true�ɂȂ�����K�E�Z���g����
    /// <summary>
    /// �K�E�Z�̌o���l�l�����ɌĂ΂�鏈��
    /// ����1 ���݂̌o���l�ʁA����2 �ڕW�̌o���l
    /// </summary>
    public event Action<float, float> SpecialEvant;

    //�������e�ϐ�
    [Header("�K�E�Z�̔\�͒l")]
    [SerializeField]
    private GameObject _specialObj;//�K�E�Z�̃I�u�W�F�N�g
    private SpecialAttackSystem _system;
    [SerializeField]
    private SpecialAttackSystem _systemSub; 
    [SerializeField]
    private float _specialTime = 3;

    private void Start()
    {
        _system = _specialObj.GetComponent<SpecialAttackSystem>();//�K�E�Z�̃X�N���v�g���擾
        _specialRequirePoint = _initRequirePoint;
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space)) //�X�y�V�����f�o�b�N�p
    //    {
    //        SpecialAttack();
    //    }
    //}

    //�o���l�ǉ������A�K�E�Z�������菈��
    public void AddSpecialExperiance(float point)
    {
        SpecialExperiancePoint += point;
        if (!_specialReady && _specialRequirePoint <= _specialExperiancePoint)
        {
            _specialReady = true;
        }
    }
    //�K�E�Z��������
    public async void SpecialAttack()
    {
        //�K�E�Z�����̏���
        if (_specialReady && _specialReady)
        {
            _specialReady = false;
            SpecialExperiancePoint = 0;
            _system.Init();//�K�E�Z�̏�����
            _systemSub.Init();
            AudioManager.Instance.PlaySE("SE_Special");//�K�E�Z��SE�Đ�
            await PauseManager.PausableWaitForSecondAsync(_specialTime);
            _system.End();
            _systemSub.End();
        }
    }
}
