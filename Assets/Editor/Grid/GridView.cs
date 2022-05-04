using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class GridView : GraphView
{
    public new class UxmlFactory : UxmlFactory<GridView, UxmlTraits> { }
    public GridView()
    {
        Insert(0, new GridBackground());
        
    }
}
