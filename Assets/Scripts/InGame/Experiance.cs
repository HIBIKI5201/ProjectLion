using UnityEngine;

public class Experiance : MonoBehaviour
{
    private float _expPoint = 0;
    private LevelContainer _levelContainer;

    public void Initialize(float point)
    {
        _expPoint = point;
        _levelContainer = FindAnyObjectByType<LevelContainer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log($"{_expPoint}");
            _levelContainer.AddExperiance(_expPoint);
            Destroy(gameObject);
        }
    }
}
