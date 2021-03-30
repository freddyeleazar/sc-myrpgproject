using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_ : Component_
{
    public GameObject bottomUI;
    [Button]
    public virtual void Awake()
    {
        bottomUI.GetComponent<BaseBottomUi_>().owner = (Base_)owner;
    }
}
