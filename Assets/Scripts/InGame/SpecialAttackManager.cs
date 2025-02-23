using SymphonyFrameWork.CoreSystem;
using System;
using UnityEngine;

public class SpecialAttackManager : MonoBehaviour
{
    //発動条件用変数
    [Header("必殺技の発動条件")]
    [Tooltip("必殺技に必要な経験値の初期値")]
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
    private bool _specialReady = false;//この変数がtrueになったら必殺技が使える
    /// <summary>
    /// 必殺技の経験値獲得時に呼ばれる処理
    /// 引数1 現在の経験値量、引数2 目標の経験値
    /// </summary>
    public event Action<float, float> SpecialEvant;

    //発動内容変数
    [Header("必殺技の能力値")]
    [SerializeField]
    private GameObject _specialObj;//必殺技のオブジェクト
    private SpecialAttackSystem _system;
    [SerializeField]
    private SpecialAttackSystem _systemSub; 
    [SerializeField]
    private float _specialTime = 3;

    private void Start()
    {
        _system = _specialObj.GetComponent<SpecialAttackSystem>();//必殺技のスクリプトを取得
        _specialRequirePoint = _initRequirePoint;
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space)) //スペシャルデバック用
    //    {
    //        SpecialAttack();
    //    }
    //}

    //経験値追加処理、必殺技発動判定処理
    public void AddSpecialExperiance(float point)
    {
        SpecialExperiancePoint += point;
        if (!_specialReady && _specialRequirePoint <= _specialExperiancePoint)
        {
            _specialReady = true;
        }
    }
    //必殺技発動処理
    public async void SpecialAttack()
    {
        //必殺技発動の処理
        if (_specialReady && _specialReady)
        {
            _specialReady = false;
            SpecialExperiancePoint = 0;
            _system.Init();//必殺技の初期化
            _systemSub.Init();
            AudioManager.Instance.PlaySE("SE_Special");//必殺技のSE再生
            await PauseManager.PausableWaitForSecondAsync(_specialTime);
            _system.End();
            _systemSub.End();
        }
    }
}
