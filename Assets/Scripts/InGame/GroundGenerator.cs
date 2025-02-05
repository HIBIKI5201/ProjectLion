using SymphonyFrameWork.CoreSystem;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject _groundPrefab;//�n�ʂ�original

    /// <summary>
    /// �n�ʂ��i�[���Ă����z��B
    /// [0]���ݏ���Ă��镔���F[1]�c���ɂ��炵�������F[2]�����ɂ��炵�������F[3]�����ɂ��炵������
    /// </summary>
    GameObject[] _grounds = new GameObject[4];

    Vector2 _groundSize;

    PlayerController _player;

    void Start()
    {
        _player = SingletonDirector.GetSingleton<PlayerController>();
        var spriteRenderer = _groundPrefab.GetComponent<SpriteRenderer>();
        _groundSize = spriteRenderer.bounds.size;

        //������
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

        //playar������Ă���I�u�W�F�N�g���ς�������̏���
        if (Mathf.Abs(distanseX) > _groundSize.x / 2)
        {
            (_grounds[0], _grounds[1]) = (_grounds[1], _grounds[0]);
            (_grounds[2], _grounds[3]) = (_grounds[3], _grounds[2]);
            //�l�̍X�V
            distanseX = _player.transform.position.x - _grounds[0].transform.position.x;
        }
        if (Mathf.Abs(distanseY) > _groundSize.y / 2)
        {
            (_grounds[0], _grounds[2]) = (_grounds[2], _grounds[0]);
            (_grounds[1], _grounds[3]) = (_grounds[3], _grounds[1]);
            //�l�̍X�V
            distanseY = _player.transform.position.y - _grounds[0].transform.position.y;
        }

        //�v���C���[���n�ʂ̂ǂ̕����ɏ���Ă��邩�̔���
        var xDir = Mathf.Sign(distanseX);
        var yDir = Mathf.Sign(distanseY);

        //player������Ă���ꏊ�ɂ���Ēn�ʂ��Đ������鏈���B
        _grounds[1].transform.position = new Vector2(_grounds[0].transform.position.x + _groundSize.x * xDir, _grounds[0].transform.position.y);
        _grounds[2].transform.position = new Vector2(_grounds[0].transform.position.x, _grounds[0].transform.position.y + _groundSize.y * yDir);
        _grounds[3].transform.position = new Vector2(_grounds[0].transform.position.x + _groundSize.x * xDir, _grounds[0].transform.position.y + _groundSize.y * yDir);
    }
}
