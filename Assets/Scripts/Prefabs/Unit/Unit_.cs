using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_ : Base_
{
    public Rigidbody2D rigidbody2D_;
    
    public override void Build()
    {
        baseType_ = BaseType_.Unit;
        graphics_.inGame_.spriteRenderer.spriteSortPoint = SpriteSortPoint.Pivot;
        base.Build();
    }
}
