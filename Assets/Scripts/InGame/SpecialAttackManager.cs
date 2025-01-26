using UnityEngine;

public class SpecialAttackManager : MonoBehaviour
{
    private float _specialExperiancePoint = 0;
    [SerializeField]
    private float _specialRequirePoint = 500;
    private bool _SpecialReady = false;//���̕ϐ���true�ɂȂ�����K�E�Z���g����

    //�K�E�Z���g���邩�ǂ���
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
