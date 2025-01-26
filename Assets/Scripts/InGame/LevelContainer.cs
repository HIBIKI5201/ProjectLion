using UnityEngine;

public class LevelContainer : MonoBehaviour
{
    private int _level = 1;
    private float _experiancePoint = 0;
    public float ExperiancePoint { get => _experiancePoint; }
    private float _nextRequirePoint = 500;
    public float NextRequirePoint { get => _nextRequirePoint; }

    private LevelUpManager _manager;

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
    }
}
