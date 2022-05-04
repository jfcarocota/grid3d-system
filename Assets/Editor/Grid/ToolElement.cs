using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class ToolElement : VisualElement
{
    public enum ToolType { grass, water, forest };

    public new class UxmlFactory : UxmlFactory<ToolElement, UxmlTraits> {}

    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlEnumAttributeDescription<ToolType> _tooltypeAttr = new UxmlEnumAttributeDescription<ToolType>
        {
            name = "tooltype-attr",
            defaultValue = ToolType.grass
        };

        UxmlColorAttributeDescription _colorAttr = new UxmlColorAttributeDescription
        {
            name = "color-attr",
            defaultValue = Color.white
        };

        UxmlStringAttributeDescription _objnameAttr = new UxmlStringAttributeDescription
        {
            name = "objname-attr",
            defaultValue = string.Empty
        };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as ToolElement;

            ate.tooltypeAttr = _tooltypeAttr.GetValueFromBag(bag, cc);
            ate.colorAttr = _colorAttr.GetValueFromBag(bag, cc);
            ate.objnameAttr = _objnameAttr.GetValueFromBag(bag, cc);
        }
    }

    public ToolType tooltypeAttr { get; set; }
    public Color colorAttr { get; set; }
    public string objnameAttr { get; set; }
    public GameObject SrcObjectElement => AssetDatabase.LoadAssetAtPath<GameObject>($"Assets/Prefabs/GridElements/{objnameAttr}.prefab");
}
