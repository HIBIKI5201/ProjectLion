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
        _grounds[0] = Instantiate(_groundPrefab, _player.transform.position, Quaternion.identity);
        _grounds[1] = Instantiate(_groundPrefab, _player.transform.position, Quaternion.identity);
        _grounds[1].transform.position = new Vector2(_player.transform.position.x, _player.transform.position.y + _groundSize.y);
        _grounds[2] = Instantiate(_groundPrefab, _player.transform.position, Quaternion.identity);
        _grounds[2].transform.position = new Vector2(_player.transform.position.x + _groundSize.x, _player.transform.position.y);
        _grounds[3] = Instantiate(_groundPrefab, _player.transform.position, Quaternion.identity);
        _grounds[3].transform.position = new Vector2(_player.transform.position.x + _groundSize.x, _player.transform.position.y + _groundSize.y);
    }

    void Update()
    {
        var yoko = _player.transform.position.x - _grounds[0].transform.position.x;
        var tate = _player.transform.position.y - _grounds[0].transform.position.y;

        //playar������Ă���I�u�W�F�N�g���ς�������̏���
        if (Mathf.Abs(yoko) > _groundSize.x / 2)
        {
            (_grounds[0], _grounds[1]) = (_grounds[1], _grounds[0]);
            (_grounds[2], _grounds[3]) = (_grounds[3], _grounds[2]);
            //�l�̍X�V
            yoko = _player.transform.position.x - _grounds[0].transform.position.x;
        }
        if (Mathf.Abs(tate) > _groundSize.y / 2)
        {
            (_grounds[0], _grounds[2]) = (_grounds[2], _grounds[0]);
            (_grounds[1], _grounds[3]) = (_grounds[3], _grounds[1]);
            //�l�̍X�V
            tate = _player.transform.position.y - _grounds[0].transform.position.y;
        }

        //�v���C���[���n�ʂ̂ǂ̕����ɏ���Ă��邩�̔���
        var xDir = yoko >= 0 ? 1 : -1;
        var yDir = tate >= 0 ? 1 : -1;

        //player������Ă���ꏊ�ɂ���Ēn�ʂ��Đ������鏈���B
        _grounds[1].transform.position = new Vector2(_grounds[0].transform.position.x + _groundSize.x * xDir, _grounds[0].transform.position.y);
        _grounds[2].transform.position = new Vector2(_grounds[0].transform.position.x, _grounds[0].transform.position.y + _groundSize.y * yDir);
        _grounds[3].transform.position = new Vector2(_grounds[0].transform.position.x + _groundSize.x * xDir, _grounds[0].transform.position.y + _groundSize.y * yDir);
    }
}
