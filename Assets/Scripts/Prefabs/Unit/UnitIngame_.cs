using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Animations;
using UnityEditor.VersionControl;
#endif
using UnityEngine;
using UnityEngine.Rendering;

public class UnitIngame_ : InGame_
{
    public void HitAttackTarget()
    {
        Unit_ unit = (Unit_)owner;
        UnitBehaviour_ unitBehaviour = (UnitBehaviour_)unit.mechanics_.behaviour_;
        unitBehaviour.HitAttackTarget();
    }
}