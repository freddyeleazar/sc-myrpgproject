using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileOwner_ : Tile
{
    [Button]
    public virtual void Build()
    {
        List<FieldInfo> fields = GetType().GetFields().ToList().FindAll(t => t.FieldType.InheritsFrom(typeof(TileComponent_)));
        foreach (FieldInfo field in fields)
        {
            TileComponent_ tileComponent = (TileComponent_)CreateInstance(field.FieldType.Name);
            tileComponent.Build(this);
            field.SetValue(this, tileComponent);
        }
    }

    [Button]
    public virtual void Empty()
    {
        List<FieldInfo> fields = GetType().GetFields().ToList().FindAll(t => t.FieldType.InheritsFrom(typeof(TileComponent_)));
        foreach (FieldInfo field in fields)
        {
            field.SetValue(this, null);
        }
    }
}
