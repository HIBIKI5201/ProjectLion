using UnityEngine;

public class SpecialAttackManager : MonoBehaviour
{
    private float _specialExperiancePoint = 0;
    [SerializeField]
    private float _specialRequirePoint = 500;
    private bool _SpecialReady = false;//‚±‚Ì•Ï”‚ªtrue‚É‚È‚Á‚½‚ç•KE‹Z‚ªg‚¦‚é

    //•KE‹Z‚ªg‚¦‚é‚©‚Ç‚¤‚©
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
