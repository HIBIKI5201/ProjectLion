using UnityEngine;

public class SpecialAttackManager : MonoBehaviour
{
    private float _specialExperiancePoint = 0;
    [SerializeField]
    private float _specialRequirePoint = 500;
    private bool _SpecialReady = false;//この変数がtrueになったら必殺技が使える

    //必殺技が使えるかどうか
    public void AddSpecialExperiance(float point)
    {
        _specialExperiancePoint += point;
        if (!_SpecialReady && _specialRequirePoint <= _specialExperiancePoint)
        {
            Debug.Log("Special Ready");
            _SpecialReady = true;
        }
    }
}
