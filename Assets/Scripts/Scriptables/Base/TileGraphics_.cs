using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class TileGraphics_ : TileComponent_
{
    public TileIngame_ ingame_;

    public override void Build(TileOwner_ tileOwner)
    {
        base.Build(tileOwner);

        List<FieldInfo> fields = GetType().GetFields().ToList().FindAll(t => t.FieldType.InheritsFrom(typeof(TileComponent_)));
        foreach (FieldInfo field in fields)
        {
            string fieldName = "TreeTile" + char.ToUpper(field.FieldType.Name.Substring("Tile".Length)[0]) + field.FieldType.Name.Substring(1 + "Tile".Length);
            TileComponent_ tileComponent = (TileComponent_)CreateInstance(fieldName);
            tileComponent.Build(tileOwner);
            field.SetValue(this, tileComponent);
        }
    }
}
