using UnityEngine;

public class Experiance : MonoBehaviour
{
    private float _expPoint = 0;
    private LevelContainer _levelContainer;
    private SpecialAttackManager _specialAttackManager;

    public void Initialize(float point)
    {
        _expPoint = point;
        _levelContainer = FindAnyObjectByType<LevelContainer>();
        _specialAttackManager = FindAnyObjectByType<SpecialAttackManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _levelContainer.AddExperiance(_expPoint);
            _specialAttackManager.AddSpecialExperiance(_expPoint);
            Destroy(gameObject);
        }
    }
}
