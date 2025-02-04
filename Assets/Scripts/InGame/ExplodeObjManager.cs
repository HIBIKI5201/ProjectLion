using UnityEngine;

public class ExplodeObjManager : MonoBehaviour
{
    private Coroutine _attackCoroutine;

    private async void Start()
    {
        await Awaitable.WaitForSecondsAsync(5);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController component))
        {
            _attackCoroutine = StartCoroutine(PlayerAttack.Attack(component, 20));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out _))
            StopCoroutine(_attackCoroutine);
    }
}