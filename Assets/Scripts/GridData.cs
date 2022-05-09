using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridData : ScriptableObject
{

    [SerializeField]
    private List<CellData> _cellData;
    [SerializeField]

    public List<CellData> CurrentCellData { get => _cellData; set => _cellData = value; }

    public static GridData NewInstance(int rows, int cols)
    {
        Debug.Log("creating instance");
        GridData instance = ScriptableObject.CreateInstance<GridData>();
        instance.CurrentCellData = new List<CellData>();
        for(int x = 0; x < rows; x++)
        {
            for(int y = 0; y < cols; y++)
            {
                instance.CurrentCellData.Add(new CellData(new Vector2(x, y), Vector4.zero, null, 10));
            }
        }
        return instance;
    }
}
