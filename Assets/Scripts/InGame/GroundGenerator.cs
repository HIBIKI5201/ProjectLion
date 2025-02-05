using SymphonyFrameWork.CoreSystem;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject _groundPrefab;//地面のoriginal

    /// <summary>
    /// 地面を格納しておく配列。
    /// [0]現在乗っている部分：[1]縦軸にずらした部分：[2]横軸にずらした部分：[3]両方にずらした部分
    /// </summary>
    GameObject[] _grounds = new GameObject[4];

    Vector2 _groundSize;

    PlayerController _player;

    void Start()
    {
        _player = SingletonDirector.GetSingleton<PlayerController>();
        var spriteRenderer = _groundPrefab.GetComponent<SpriteRenderer>();
        _groundSize = spriteRenderer.bounds.size;

        //初期化
        for (var i = 0; i < 4; i++)
        {
            _grounds[i] = Instantiate(_groundPrefab, _player.transform.position, Quaternion.identity);
        }
        _grounds[1].transform.position = new Vector2(_player.transform.position.x, _player.transform.position.y + _groundSize.y);
        _grounds[2].transform.position = new Vector2(_player.transform.position.x + _groundSize.x, _player.transform.position.y);
        _grounds[3].transform.position = new Vector2(_player.transform.position.x + _groundSize.x, _player.transform.position.y + _groundSize.y);
    }

    void Update()
    {
        var distanseX = _player.transform.position.x - _grounds[0].transform.position.x;
        var distanseY = _player.transform.position.y - _grounds[0].transform.position.y;

        //playarが乗っているオブジェクトが変わった時の処理
        if (Mathf.Abs(distanseX) > _groundSize.x / 2)
        {
            (_grounds[0], _grounds[1]) = (_grounds[1], _grounds[0]);
            (_grounds[2], _grounds[3]) = (_grounds[3], _grounds[2]);
            //値の更新
            distanseX = _player.transform.position.x - _grounds[0].transform.position.x;
        }
        if (Mathf.Abs(distanseY) > _groundSize.y / 2)
        {
            (_grounds[0], _grounds[2]) = (_grounds[2], _grounds[0]);
            (_grounds[1], _grounds[3]) = (_grounds[3], _grounds[1]);
            //値の更新
            distanseY = _player.transform.position.y - _grounds[0].transform.position.y;
        }

        //プレイヤーが地面のどの部分に乗っているかの判定
        var xDir = Mathf.Sign(distanseX);
        var yDir = Mathf.Sign(distanseY);

        //playerが乗っている場所によって地面を再生成する処理。
        _grounds[1].transform.position = new Vector2(_grounds[0].transform.position.x + _groundSize.x * xDir, _grounds[0].transform.position.y);
        _grounds[2].transform.position = new Vector2(_grounds[0].transform.position.x, _grounds[0].transform.position.y + _groundSize.y * yDir);
        _grounds[3].transform.position = new Vector2(_grounds[0].transform.position.x + _groundSize.x * xDir, _grounds[0].transform.position.y + _groundSize.y * yDir);
    }
}
