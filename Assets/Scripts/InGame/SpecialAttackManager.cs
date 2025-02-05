using SymphonyFrameWork.CoreSystem;
using System;
using UnityEngine;

public class SpecialAttackManager : MonoBehaviour
{
    //発動条件用変数
    [Header("必殺技の発動条件")]
    [SerializeField]
    private float _specialRequirePoint = 100;
    public float SpecialRequirePoint { get => _specialRequirePoint; }

    private float _specialExperiancePoint = 0;
    public float SpecialExperiancePoint { get => _specialExperiancePoint; }
    private bool _specialReady = false;//この変数がtrueになったら必殺技が使える
    public event Action<float> SpecialEvant;

    //発動内容変数
    [Header("必殺技の能力値")]
    [SerializeField]
    private GameObject _specialObj;//必殺技のオブジェクト
    private SpecialAttackSystem _system;
    [SerializeField]
    private float _specialTime = 3;

    private void Start()
    {
        _system = _specialObj.GetComponent<SpecialAttackSystem>();//必殺技のスクリプトを取得
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space)) //スペシャルデバック用
        //{
        //    SpecialAttack();
        //}
    }
    //経験値追加処理、必殺技発動判定処理
    public void AddSpecialExperiance(float point)
    {
        _specialExperiancePoint += point;
        SpecialEvant?.Invoke(_specialRequirePoint - _specialExperiancePoint);
        if (!_specialReady && _specialRequirePoint <= _specialExperiancePoint)
        {
            _specialReady = true;
        }
    }
    //必殺技発動処理
    public async void SpecialAttack()
    {
        //必殺技発動の処理
        if (_specialReady && !_specialObj.activeInHierarchy)
        {
            _system.init();//必殺技の初期化
            _specialObj.SetActive(true);
            await PauseManager.PausableWaitForSecondAsync(_specialTime);
            _specialObj.SetActive(false);
            //必殺技発動終了の処理
            _specialReady = false;
            SpecialEvant?.Invoke(0);
            _specialExperiancePoint = 0;
        }
    }
}
