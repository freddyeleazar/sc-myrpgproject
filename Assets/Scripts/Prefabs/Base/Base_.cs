using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_ : Owner_
{
    public Graphics_ graphics_;
    public Mechanics_ mechanics_;
    public Inventory_ inventory_;

    public BaseType_ baseType_;
    public string objectType;
}

public enum BaseType_
{
    Item,
    Unit,
    Tree
}
