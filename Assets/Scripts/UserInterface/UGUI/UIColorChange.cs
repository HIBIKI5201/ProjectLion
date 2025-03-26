using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIColorChange : MonoBehaviour
{
    [SerializeField] List<Color> _colors = new List<Color>();
    [SerializeField] List<Graphic> _graphics = new List<Graphic>();
    [SerializeField] int _graphicsIndex = 0;
    
    public void ChangeColor(int index)
    {
        if (_colors.Count <= index)
        {
            Debug.LogError("_colors is out of range.");
            return;
        }
        
        _graphics[_graphicsIndex].color = _colors[index];
    }
}
