using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class TileComponent_ : ScriptableObject
{
    [HideInInspector]
    public TileOwner_ tileOwner;

    public virtual void Build(TileOwner_ tileOwner)
    {
        this.tileOwner = tileOwner;
        List<FieldInfo> fields = GetType().GetFields().ToList().FindAll(t => t.FieldType.InheritsFrom(typeof(TileComponent_)));
        foreach (FieldInfo field in fields)
        {
            TileComponent_ tileComponent_ = (TileComponent_)CreateInstance(field.FieldType);
            tileComponent_.Build(tileOwner);
            field.SetValue(this, tileComponent_);
        }
    }
}
