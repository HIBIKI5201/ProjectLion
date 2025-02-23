using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


[UxmlElement]
public partial class TextureMask : VisualElement
{
    [UxmlAttribute] Texture2D _texture;
    [UxmlAttribute] Vector2 _textureOffset;
    [UxmlAttribute,Tooltip("未実装")] private float _textureScale = 1f;

    [UxmlAttribute,Tooltip("時計回りに頂点座標を入力してください")] Vector2[] _vertexData;

    public TextureMask()
    {
        generateVisualContent += GenerateMesh;
    }

    void GenerateMesh(MeshGenerationContext context)
    {
        if (_vertexData is null || !_texture) return;

        var maxX = contentRect.width;
        var maxY = contentRect.height;
        Vector2 originPos = new Vector2(maxX / 2, maxY / 2);

        var vertex = new Vertex[_vertexData.Length + 1];
        List<ushort> triangles = new();

        vertex[0].position = originPos;
        vertex[0].uv = new Vector2(originPos.x / maxX, 1 - originPos.y / maxY) + _textureOffset;
        vertex[0].tint = Color.white;

        for (int i = 1; i < vertex.Length; i++)
        {
            Vector3 position = _vertexData[i - 1] * new Vector2(maxX, maxY);
            vertex[i].position = position;
            vertex[i].uv = new Vector2((position.x / maxX) , (1 - position.y / maxY) ) +
                           _textureOffset;
            vertex[i].tint = Color.white;

            if (i < 2) continue;

            triangles.Add((ushort)(0));
            triangles.Add((ushort)(i - 1));
            triangles.Add((ushort)(i));

            if (i != vertex.Length - 1) continue;

            triangles.Add(0);
            triangles.Add((ushort)i);
            triangles.Add(1);
        }

        _texture.wrapMode = TextureWrapMode.Clamp;
        if (triangles.Count < 3)
        {
            Debug.Log("No triangles found");
            return;
        }

        MeshWriteData mesh = context.Allocate(vertex.Length, triangles.Count, _texture);

        mesh.SetAllVertices(vertex);
        mesh.SetAllIndices(triangles.ToArray());
    }
}