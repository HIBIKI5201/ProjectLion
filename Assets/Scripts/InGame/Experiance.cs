using UnityEngine;

public class Experiance : LevelContainer
{
    //���݂͈ȉ��̃V���A���C�Y�Ōo���l�ʂ�ς��Ă��邽��,�o���l�ʂ��Ƃɕʂ̃v���n�u���쐬����K�v������
    [SerializeField]
    private float _expPoint = 0;
    //public float ExperiancePoint { get => _expPoint; }
    private LevelContainer _levelContainer;

    private void Start()
    {
        _levelContainer = FindAnyObjectByType<LevelContainer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AddExperiance(_expPoint);
            Destroy(gameObject);
        }
    }
    private void AddExperiance(float point)
    {
        _levelContainer.AddExperiance(point);
    }
}
