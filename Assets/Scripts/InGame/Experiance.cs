using UnityEngine;

public class Experiance : LevelContainer
{
    //現在は以下のシリアライズで経験値量を変えているため,経験値量ごとに別のプレハブを作成する必要がある
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
