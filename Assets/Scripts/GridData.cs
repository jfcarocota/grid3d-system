using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridData : ScriptableObject
{
    [SerializeField]
    private GameObject[,] _gridElements;
    [SerializeField]
    private Color[,] _cellColors;
    private string _name;

    List<CellData> _cellData;
    public GameObject[,] GridElements { get => _gridElements; set => _gridElements = value; }
    public Color[,] CurrentCellColors { get => _cellColors; set => _cellColors = value; }
    public string Name { get => _name; set => _name = value; }
    public List<CellData> CurrentCellData { get => _cellData; set => _cellData = value; }

    public static GridData NewInstance(GameObject[,] gridElements, Color[,] cellColors, string name)
    {
        GridData instance = ScriptableObject.CreateInstance<GridData>();
        instance.GridElements = gridElements;
        instance.CurrentCellColors = cellColors;
        instance.Name = name;
        return instance;
    }

    public static GridData NewInstance(int rows, int cols)
    {
        GridData instance = ScriptableObject.CreateInstance<GridData>();
        instance.CurrentCellData = new List<CellData>();
        for(int x = 0; x < rows; x++)
        {
            for(int y = 0; y < cols; y++)
            {
                instance.CurrentCellData.Add(new CellData(new Vector2(x, y), new Color(0f, 0f, 0f, 0f), null));
            }
        }
        return instance;
    }
}
