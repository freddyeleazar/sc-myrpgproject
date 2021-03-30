using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class GroundIngame_ : TileComponent_
{
    [PreviewField(Height = 63)]
    public List<Sprite> sprites = new List<Sprite>();

    public override void Build(TileOwner_ tileOwner)
    {
        base.Build(tileOwner);
        sprites = Resources.LoadAll<Sprite>("Sprites/Grounds/"+tileOwner.name).ToList();
    }
}
