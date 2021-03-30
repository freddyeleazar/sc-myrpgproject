using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeStats_ : Stats_
{
    [ShowInInspector]
    public Biome biome;

    public TreeName treeName;
    public TreeAction treeAction;

    public override void Build(Owner_ owner)
    {
        base.Build(owner);
        biome = new Biome(((Tree_)owner).biomeType);
    }
}

public enum TreeName
{
    Apple,
    Chestnut,
    CommonPalmTree,
    Pine,
    SmallPalmTree
}

public enum TreeAction
{
    Idle,
    FallingDown,
    Chopped
}