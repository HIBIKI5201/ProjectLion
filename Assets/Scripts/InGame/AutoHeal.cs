using UnityEngine;
using SymphonyFrameWork.CoreSystem;

public class AutoHeal : MonoBehaviour
{
    private PlayerController controller;
    private float _timer = 0;
    private float _interval = 5;

    private bool _death;

    [SerializeField]
    private float _initHealValue;
    public float InitHealValue { get => _initHealValue; }
    private float _healValue;
    public float Healvalue { get => _healValue; set { _healValue = value; } }

    void Start()
    {
        _healValue = _initHealValue;
        controller = SingletonDirector.GetSingleton<PlayerController>();
        controller.OnDeath += () => _death = true;
    }

    void Update()
    {
        if (_timer + _interval < Time.time &&
            controller.CurrentHealth + Healvalue <= controller.MaxHealth &&
            !_death)
        {
            _timer = Time.time;
            controller.AddHealth(Healvalue);
        }
    }
}
