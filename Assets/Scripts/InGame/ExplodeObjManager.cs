using SymphonyFrameWork.CoreSystem;
using UnityEngine;

public class ExplodeObjManager : MonoBehaviour
{
    [SerializeField]
    private float _damage = 10;
    [SerializeField]
    private float _time = 5;
    private Coroutine _attackCoroutine;

    private async void Start()
    {
        try
        {
            await PauseManager.PausableWaitForSecondAsync(_time, destroyCancellationToken);
            Destroy(gameObject);
        }
        catch { }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController component))
        {
            _attackCoroutine = StartCoroutine(PlayerAttack.Attack(component, _damage));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out _))
            if (_attackCoroutine != null) StopCoroutine(_attackCoroutine);
    }
}