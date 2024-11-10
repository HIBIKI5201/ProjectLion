using UnityEngine;

public class AbillityManager : MonoBehaviour
{
    float _theta = 0;
    float _radius = 5;

    [SerializeField]
    float _speed = 5;

    private void Update()
    {
        Vector2 pos = PlayerController.player.transform.position + new Vector3(Mathf.Cos(_theta) * _radius ,Mathf.Sin(_theta) * _radius);
        transform.position = pos;
        _theta += Time.deltaTime * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.TryGetComponent(out EnemyManager enemy))
            {
                enemy.AddDamage(50);
            }
        }
    }
}
