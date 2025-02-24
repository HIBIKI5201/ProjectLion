using UnityEngine;
using SymphonyFrameWork.CoreSystem;

public class AutoHeal : MonoBehaviour
{
    private float _currentHealth;//Œ»Ý‚ÌHP‚Ì’l
    private float _timer = 0;
    private float _interval = 5;

    [SerializeField]
    private float _initHealValue;
    public float InitHealValue { get => _initHealValue; }
    private float _healValue;
    public float Healvalue { get => _healValue; set { _healValue = value; } }

    void Start()
    {
        _healValue = _initHealValue;
        _currentHealth = SingletonDirector.GetSingleton<PlayerController>().CurrentHealth;
    }

    void Update()
    {
        if (_timer + _interval < Time.time)
        {
            Debug.Log(_healValue);
            _timer = Time.time;
            _currentHealth += _healValue;
        }
    }
}
