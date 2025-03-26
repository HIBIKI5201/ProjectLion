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

    MobBase<MobData_S> _player;

    float _curentHelth;

    void Start()
    {
        _player = SingletonDirector.GetSingleton<PlayerController>();
        if(_camera == null)_camera= Camera.main;
    }
    private void Update()
    {
        if(!_player)return;

        _hpBar.transform.position = Camera.main.WorldToScreenPoint(_player.transform.position - new Vector3(0, _yOffset, 0));


        if (_player.CurrentHealth != _curentHelth)//ToDo:HERE アクションに変更
        {
            SliderUpdate(_player.CurrentHealth / _player.MaxHealth);
        }
    }
    void SliderUpdate(float normalizedCurent)
    {
        _hpBar.value = normalizedCurent;
    }
}
