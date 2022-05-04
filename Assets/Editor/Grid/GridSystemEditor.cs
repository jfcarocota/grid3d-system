using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(GridSystem))]
public class GridSystemEditor : Editor
{
    private GridSystem _gridSystem;
    private VisualElement _root;
    private VisualTreeAsset _treeAsset;
    private StyleSheet _styleSheet;
    private ObjectField _sourceObject;
    private IMGUIContainer _arrayContainer;

    private VisualTreeAsset _cellTreeAsset;
    private VisualElement _cellUxml;
    private StyleSheet _cellStyle;
    private VisualElement _cellElement;

    private VisualTreeAsset _rowTreeAsset;
    private VisualElement _rowUxml;
    private StyleSheet _rowStyle;
    private VisualElement _rowElement;


    private IntegerField _n;
    private IntegerField _m;
    private FloatField _cellWitdth;
    private FloatField _cellHeight;
    private Button _btnGenerateGrid;
    private VisualElement _grid;

    private ToolElement.ToolType _toolType;
    private List<ToolElement> _tools;

    private ListView _objectsList;
    private Color _currentCellColor;
    private GameObject _currentGridElement;
    private Label _gridAssetPath;
    private ObjectField _gridData;
    private string _path;

    private CellElement[,] _cells;

    private void OnEnable()
    {
        _gridAssetPath = new Label();
        _gridAssetPath.BindProperty(serializedObject.FindProperty("_gridAssetPath"));

        _tools = new List<ToolElement>();
        _toolType  = ToolElement.ToolType.grass;
        _cellElement = new VisualElement();
        _rowElement = new VisualElement();
        _cellUxml = new VisualElement();
        _rowUxml = new VisualElement();
        _root = new VisualElement();
        _gridSystem = (GridSystem)target;
        _treeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/Grid/GridSystem.uxml");
        _styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/Grid/GridSystem.uss");
        _treeAsset.CloneTree(_root);
        _root.styleSheets.Add(_styleSheet);

        _grid =  _root.Q<VisualElement>("gridScrollView");
        _btnGenerateGrid = _root.Q<Button>("btnGenerate");
        _n = _root.Q<IntegerField>("n");
        _m = _root.Q<IntegerField>("m");
        _cellWitdth = _root.Q<FloatField>("cellWitdth");
        _cellHeight = _root.Q<FloatField>("cellHeight");
        _objectsList = _root.Q<ListView>("objecstList");

        _n.BindProperty(serializedObject.FindProperty("_n"));
        _m.BindProperty(serializedObject.FindProperty("_m"));
        _cellWitdth.BindProperty(serializedObject.FindProperty("_cellWidth"));
        _cellHeight.BindProperty(serializedObject.FindProperty("_cellHeight"));

        _cellTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/Grid/Cell/Cell.uxml");
        _cellStyle = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/Grid/Cell/Cell.uss");
        _cellTreeAsset.CloneTree(_cellUxml);
        _cellUxml.styleSheets.Add(_cellStyle);
        _cellElement = _cellUxml.Q<VisualElement>("cell");

        _rowTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/Grid/Row/Row.uxml");
        _rowStyle = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/Grid/Row/Row.uss");
        _rowTreeAsset.CloneTree(_rowUxml);
        _rowUxml.styleSheets.Add(_rowStyle);
        _rowElement = _cellUxml.Q<VisualElement>("row");
        
        _btnGenerateGrid.clicked += OnGeneratedGrid;

        // Finding tools list
        _tools = _root.Query<ToolElement>("tool").ToList();
        _currentCellColor = _tools[0].colorAttr;
        _currentGridElement = _tools[0].SrcObjectElement;
        _tools.ForEach(tool => tool.RegisterCallback<ClickEvent>(e =>
        {
            _toolType = tool.tooltypeAttr;
            _currentCellColor = tool.colorAttr;
            _currentGridElement = tool.SrcObjectElement;
        }));

        _gridData = _root.Q<ObjectField>("gridData");
        _gridData.objectType = typeof(GridData);
        _gridData.BindProperty(serializedObject.FindProperty("_gridData"));

        _cells = new CellElement[_gridSystem.GetN, _gridSystem.GetM];
    }

    private void OnGeneratedGrid()
    {
        _gridSystem.GenerateGrid();
    }

    public override VisualElement CreateInspectorGUI()
    {
            // generate paintable grid 2D N*M
            for (int y = 0; y < _gridSystem.GetM; y++)
            {
                VisualElement row = new VisualElement();
                row.AddToClassList("row");
                _grid.contentContainer.Add(row);
                for (int x = 0; x < _gridSystem.GetN; x++)
                {
                    CellElement cell = new CellElement();
                    cell.AddToClassList("cell");
                    cell.posxAttr = x;
                    cell.posyAttr = y;
                    cell.style.width = _cellWitdth.value;
                    cell.style.height = _cellHeight.value;
                    _cells[x, y] = cell;
                    row.Add(cell);

                    if (_gridSystem.CurrentGridData != null)
                    {
                        CellData cellData = _gridSystem.CurrentGridData.CurrentCellData.Find(item => item.Position == new Vector2(cell.posxAttr, cell.posyAttr));
                        if(cellData.CellColor != null)
                        {
                            cell.style.backgroundColor = cellData.CellColor;
                        }
                    }
                    cell.RegisterCallback<ClickEvent>(e => {

                        if (_gridSystem.CurrentGridData == null)
                        {
                            _path = EditorUtility.SaveFilePanelInProject("Create a grid asset", "GridData", "asset", "Grid created");
                            if (!string.IsNullOrEmpty(_path))
                            {
                                GridData gridData = GridData.NewInstance(_gridSystem.GetN, _gridSystem.GetM);
                                _gridData.value = gridData;


                                AssetDatabase.CreateAsset(gridData, _path);
                                CellData cellData = gridData.CurrentCellData.Find(item => item.Position == new Vector2(cell.posxAttr, cell.posyAttr));
                                cellData.CellColor = _currentCellColor;
                                cellData.GridElement = _currentGridElement;
 
                                AssetDatabase.SaveAssets();
                                AssetDatabase.Refresh();
                            }
                        }
                        else
                        {
                            CellData cellData = _gridSystem.CurrentGridData.CurrentCellData.Find(item => item.Position == new Vector2(cell.posxAttr, cell.posyAttr));
                            cellData.CellColor = _currentCellColor;
                            cellData.GridElement = _currentGridElement;
;
                        }
                        cell.style.backgroundColor = _currentCellColor;
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                    });
                }
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

        return _root;
    }
}
