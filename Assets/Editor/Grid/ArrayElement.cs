using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class ArrayElement : ListView
{
    public new class UxmlFactory : UxmlFactory<ArrayElement, UxmlTraits> { }

    public new class UxmlTraits : ListView.UxmlTraits
    {
        UxmlIntAttributeDescription _size = new UxmlIntAttributeDescription
        {
            name = "size-attr",
            defaultValue = 0
        };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as ArrayElement;

            ate.sizeAttr = _size.GetValueFromBag(bag, cc);
            ate.reorderable = true;
            ate.showAddRemoveFooter = true;
        }
    }

    public int sizeAttr { get; set; }
}
