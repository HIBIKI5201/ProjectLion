using System;
using UnityEngine;

public class LevelContainer : MonoBehaviour
{
    private int _level = 1;

    private LevelUpManager _manager;

    private float _experiancePoint = 0;
    private float _nextRequirePoint = 500;
    public float ExperiancePoint { get => _experiancePoint; }
    public float NextRequirePoint { get => _nextRequirePoint; }

    /// <summary>
    /// �o���l�擾���ɌĂ΂�鏈��
    /// ����1�@���݂̌o���l  ����2�@���̌o���l�B
    /// </summary>
    public event Action<float, float> OnAddExperiance;

    private void Awake()
    {
        _manager = GetComponent<LevelUpManager>();
    }

    public void AddExperiance(float point)
    {
        _experiancePoint += point;
        if (_nextRequirePoint <= _experiancePoint)
        {
            _experiancePoint = _experiancePoint - _nextRequirePoint;
            _nextRequirePoint = 500 * Mathf.Pow(1.1f, ++_level);
            _manager.GetNewItem();
        }
        OnAddExperiance?.Invoke(_experiancePoint, _nextRequirePoint);
    }
}
