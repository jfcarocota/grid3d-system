using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GridSystem : MonoBehaviour
{
    [SerializeField]
    private int _n = 0;
    [SerializeField]
    private int _m = 0;

    private string[,] _grid;

    [SerializeField]
    private GameObject[,] _srcObjects;

    private float _posX = 0f;
    private float _posY = 0f;
    [SerializeField]
    private float _cellWidth = 1f;
    [SerializeField]
    private float _cellHeight = 1f;
    [SerializeField]
    string _gridAssetPath;
    [SerializeField]
    private GridData _gridData;

    public void GenerateGrid()
    {
        //_srcObjects = _gridData.GridElements;
        if(_gridData != null)
        {
            Debug.Log("create grid");
            for (int y = 0; y < _n; y++)
            {
                for (int x = 0; x < _m; x++)
                {
                    _posX = _cellWidth * x;
                    _posY = _cellHeight * y;
                    CellData cellData = _gridData.CurrentCellData.Find(item => item.Position == new Vector2(x, y));

                    if (cellData.GridElement != null)
                    {
                        Transform tGridObject = Instantiate(cellData.GridElement, transform, true).transform;
                        tGridObject.localPosition = new Vector3(_posX, 0f, -_posY);
                    }
                }
            }
        }
    }

    private void SelectGridObject()
    {

    }

    public int GetN => _n;
    public int GetM => _m;

    public string[,] Grid { get => _grid; set => _grid = value; }
    public GameObject[,] SrcObjects { get => _srcObjects; set => _srcObjects = value; }
    public string GridAssetPath { get => _gridAssetPath; set => _gridAssetPath = value; }
    public GridData CurrentGridData { get => _gridData; set => _gridData = value; }
}
