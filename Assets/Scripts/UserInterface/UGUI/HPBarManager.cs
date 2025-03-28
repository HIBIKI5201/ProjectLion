using SymphonyFrameWork.CoreSystem;
using UnityEngine;
using UnityEngine.UI;

public class HPBarManager : MonoBehaviour
{
    [SerializeField]
    Canvas _hpbarCanvas;
    [SerializeField]
    Slider _hpBar;
    [SerializeField]
    float _yOffset;
    [SerializeField]
    Camera _camera;

    PlayerController _player;

    float _curentHelth;

    void Start()
    {
        _player = SingletonDirector.GetSingleton<PlayerController>();
        _player.OnDamage += hp=>
        {
            SliderUpdate(hp / _player.MaxHealth);
            Debug.Log("Damaged");
        };
        _player.OnDeath += () =>
        {
            SliderUpdate(0);
            Debug.Log("Death");
        };
    }
    private void Update()
    {
        if(!_player)return;

        _hpBar.transform.position = Camera.main.WorldToScreenPoint(_player.transform.position - new Vector3(0, _yOffset, 0));
    }
    void SliderUpdate(float normalizedCurent)
    {
        _hpBar.value = normalizedCurent;
    }
}
