using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CellElement : VisualElement
{
    public new class UxmlFactory : UxmlFactory<CellElement, UxmlTraits> { }

    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlIntAttributeDescription _posxAttr = new UxmlIntAttributeDescription
        {
            name = "posx-attr",
            defaultValue = 0
        };

        UxmlIntAttributeDescription _posyAttr = new UxmlIntAttributeDescription
        {
            name = "posy-attr",
            defaultValue = 0
        };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as CellElement;

            ate.posxAttr = _posxAttr.GetValueFromBag(bag, cc);
            ate.posyAttr = _posyAttr.GetValueFromBag(bag, cc);
        }
    }

    public int posxAttr { get; set; }
    public int posyAttr { get; set; }
}
