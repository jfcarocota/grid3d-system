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

    private int _number;

    public CellData(Vector2 position, Color cellColor, GameObject gridElement, int number)
    {
        _position = position;
        _cellColor = cellColor;
        _gridElement = gridElement;
        _number = number;
    }

    public Vector2 Position { get => _position; set => _position = value; }
    public Color CellColor { get => _cellColor; set => _cellColor = value; }
    public GameObject GridElement { get => _gridElement; set => _gridElement = value; }
    public int Number { get => _number; set => _number = value; }
}
