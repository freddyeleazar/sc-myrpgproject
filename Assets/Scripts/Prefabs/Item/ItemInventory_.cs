using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory_ : Inventory_
{
    public override void CreateSlots()
    {
        base.CreateSlots();
        CreateSlot(SlotType.Baggage);//Crear slot genérico
    }

    public override void SetWeightCapacity()
    {
        base.SetWeightCapacity();
        weightCapacity = ((ItemStats_)((Item_)owner).mechanics_.stats_).weightCapacity;
    }
}
