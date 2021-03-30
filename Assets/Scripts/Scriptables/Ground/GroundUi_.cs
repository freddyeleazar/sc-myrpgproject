using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GroundUi_ : TileComponent_
{
    public GameObject bottomUi;

    private void Awake()
    {
        bottomUi = Resources.Load("Ground Bottom UI") as GameObject;
        bottomUi.GetComponent<GroundBottomUi_>().owner = (Ground_)tileOwner;
    }
}
