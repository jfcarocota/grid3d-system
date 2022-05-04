using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    private Renderer _rederer;
    private Color _focusColor;

    private void Awake()
    {
        _rederer = GetComponent<Renderer>();
        _focusColor = _rederer.material.GetColor("_FocusColor");
    }

    private void Start()
    {
        _rederer.material.SetColor("_FocusColor", Color.black);
    }

    private void OnMouseEnter()
    {
        if (_rederer != null)
        {
            _rederer.material.SetColor("_FocusColor", _focusColor);
        }
    }

    private void OnMouseExit()
    {
        if (_rederer != null)
        {
            _rederer.material.SetColor("_FocusColor", Color.black);
        }
    }
}
