using UnityEngine;

public class ExplodeEnemy : EnemyManager
{
    [SerializeField]
    private GameObject _explodeObj;

    protected override void DeathBehaviour()
    {
        base.DeathBehaviour();
        Instantiate(_explodeObj, transform.position, Quaternion.identity);
    }
}
