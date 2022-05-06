using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CellColor
{
    [SerializeField]
    float r;
    [SerializeField]
    float g;
    [SerializeField]
    float b;
    [SerializeField]
    float a;

    public Color GetColor => new Color(r, g, b, a);
    public void SetColor(Color color)
    {
        r = color.r;
        g = color.g;
        b = color.b;
        a = color.a;
    }
}
