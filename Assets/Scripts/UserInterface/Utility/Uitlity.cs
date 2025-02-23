using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor.UIElements;
#endif

namespace LionUitlity
{
    public static class Uitlity
    {
        public static float Remap(this float value, float from1 = 0, float to1 = 10, float from2 = -0.1f,
            float to2 = 0.1f)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
    }

#if UNITY_EDITOR
    public class VertexPositionDatas : UxmlAttributeConverter<Vector2[]>
    {
        public override Vector2[] FromString(string value)
        {
            var sprit = value.Replace(" ", "").Replace("(", "").Replace(")", "").Split('|');
            Vector2[] output = new Vector2[sprit.Length];
            for (int i = 0; i < sprit.Length; i++)
            {
                var n = Array.ConvertAll(sprit[i].Trim(')').Split(','), float.Parse);
                output[i] = new Vector2(n[0], n[1]);
            }

            return output;
        }

        public override string ToString(Vector2[] value) =>
            System.FormattableString.Invariant($"{string.Join('|', value)}");
    }
#endif
}