using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_ : Base_
{
    public ItemType itemType;
    public SlotType equippableSlot;
    public Slot_ occupyingSlot;

    public int damage;

    public override void Build()
    {
        baseType_ = BaseType_.Item;
        graphics_.inGame_.spriteRenderer.spriteSortPoint = SpriteSortPoint.Pivot;
        base.Build();
    }
}

public enum ItemType
{
    Apparel,
    BodySpecialFeature,
    FullBody,
    Weapon
}
