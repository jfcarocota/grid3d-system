using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CellData
{
    [SerializeField]
    private Vector2 _position;
    [SerializeField]
    private Color _cellColor;
    [SerializeField]
    private GameObject _gridElement;

    public CellData(Vector2 position, Color cellColor, GameObject gridElement)
    {
        _position = position;
        _cellColor = cellColor;
        _gridElement = gridElement;
    }

    public Vector2 Position { get => _position; set => _position = value; }
    public Color CellColor { get => _cellColor; set => _cellColor = value; }
    public GameObject GridElement { get => _gridElement; set => _gridElement = value; }
}
