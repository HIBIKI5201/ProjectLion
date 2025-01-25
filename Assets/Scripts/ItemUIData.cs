using UnityEngine;

[CreateAssetMenu(fileName = "ItemUIData", menuName = "ItemUIData")]
public class ItemUIData : ScriptableObject
{
    [SerializeField] ItemKind _kind;
    [SerializeField] string _text;
    [SerializeField] string _information;
    [SerializeField] Texture2D _texture;
    public string Name { get => _text; }
    public string Information { get => _information; }
    public Texture2D Texture { get => _texture; }
    public ItemKind Kind { get => _kind; }
}