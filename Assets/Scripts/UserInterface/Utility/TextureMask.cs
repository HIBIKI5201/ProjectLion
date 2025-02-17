using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class TextureMask : VisualElement
{
    [UxmlAttribute, Range(0, 1)] private float _percentWide;
    [UxmlAttribute, Range(0, 1)] private float _percentHigh;

    [UxmlAttribute] private Sprite _maskSprite;
    [UxmlAttribute] Vector2 _textureOffset;

    private int _vartexCount = 4;
    private int _edgeCount = 4;

    [UxmlAttribute] Texture2D _texture;
    public TextureMask()
    {
        generateVisualContent += GenerateMesh;
    }

    void GenerateMesh(MeshGenerationContext context)
    {
        var maxX = contentRect.width;
        var maxY = contentRect.height;

        var vertex = new Vertex[4];
        ushort[] triangles = { 0, 1, 2, 2, 3, 0 };

        {
            vertex[0].position = new Vector3(0, maxY / 2);
            vertex[1].position = new Vector3(maxX / 2, 0);
            vertex[2].position = new Vector3(maxX, maxY / 2);
            vertex[3].position = new Vector3(maxX / 2, maxY);
        }

        {
            vertex[0].uv = new Vector2(0, .5f)+ _textureOffset;
            vertex[1].uv = new Vector2(.5f, 1)+ _textureOffset;
            vertex[2].uv = new Vector2(1, .5f) + _textureOffset;
            vertex[3].uv = new Vector2(.5f, 0) + _textureOffset;
        }
        {
            vertex[0].tint = Color.white;
            vertex[1].tint = Color.white;
            vertex[2].tint = Color.white;
            vertex[3].tint = Color.white;
        }

        _texture.wrapMode = TextureWrapMode.Clamp;

        MeshWriteData mesh = context.Allocate(vertex.Length, triangles.Length, _texture);

        mesh.SetAllVertices(vertex);
        mesh.SetAllIndices(triangles);
    }
}
