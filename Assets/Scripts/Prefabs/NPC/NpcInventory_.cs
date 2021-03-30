using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInventory_ : UnitInventory_
{
    [Button]
    public override void CreateSlots()
    {
        base.CreateSlots();
        Enum.GetValues(typeof(SlotType)).FilterCast<SlotType>().ForEach(t => CreateSlot(t));
    }

    public override void SetWeightCapacity()
    {
        base.SetWeightCapacity();
        weightCapacity = (((NpcStats_)((Npc_)owner).mechanics_.stats_).strenght * 500) / 100;
    }
}
