using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree_ : Base_
{
    public TreeName treeName;
    public BiomeType biomeType;

    public override void Build()
    {
        baseType_ = BaseType_.Tree;
        graphics_.inGame_.spriteRenderer.spriteSortPoint = SpriteSortPoint.Pivot;
        base.Build();
    }
}
