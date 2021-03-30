using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory_ : Component_
{
    public List<Slot_> slots;
    public Slot_ genericSlot;
    public float weightCapacity;
    public float currentWeight;

    public PlayerUi_ playerUi;

    #region Build Methods
    public override void Build(Owner_ owner)
    {
        base.Build(owner);
        CreateSlots();
        genericSlot = slots.Find(t => t.slotType == SlotType.Baggage);
        SetWeightCapacity();
        currentWeight = GetCurrentWeight();
    }

    public override void Empty()
    {
        base.Empty();
        DeleteSlots();
    }
    #endregion

    #region Slots Controller Methods
    [Button]
    public virtual void CreateSlots()
    {
        DeleteSlots();
        slots = new List<Slot_>();
    }

    [Button]
    public void DeleteSlots()
    {
        slots = new List<Slot_>();
        GetComponentsInChildren<Slot_>().ForEach(t => DeleteSlot(t));
    }

    public void DeleteSlot(Slot_ slot)
    {
        DestroyImmediate(slot.gameObject, true);
    }

    public void CreateSlot(SlotType slotType)
    {
        Slot_ slot = new GameObject(slotType.ToString()).AddComponent<Slot_>().CreateSlot(slotType, owner);
        slot.transform.parent = transform;
        slot.transform.localPosition = Vector3.zero;
        slots.Add(slot);
    }
    #endregion

    #region Weight Controller Methods
    public virtual void SetWeightCapacity()
    {
        weightCapacity = 0;
    }

    public float GetCurrentWeight()
    {
        return slots.Sum(t => t.currentWeight);
    }

    public void RefreshCurrentWeight()
    {
        currentWeight = GetCurrentWeight();
    }
    #endregion

    #region Item Info Methods
    public List<Item_> GetEquippedItems()
    {
        List<Item_> equippedItems = new List<Item_>();
        foreach (Slot_ slot in slots)
        {
            equippedItems.AddRange(slot.storedItems.FindAll(t => t.occupyingSlot.slotType == t.equippableSlot));
        }
        return equippedItems;
    }

    public bool IsInInventory(Item_ item)
    {
        return item.occupyingSlot != null;
    }

    public bool IsStored(Item_ item)
    {
        if (IsInInventory(item))
        {
            return item.occupyingSlot == genericSlot;
        }
        else
        {
            return false;
        }
    }

    public bool IsEquipped(Item_ item)
    {
        if (IsInInventory(item))
        {
            return item.occupyingSlot.slotType == item.equippableSlot;
        }
        else
        {
            return false;
        }
    }
    #endregion

    #region Item Actions
    [Button]
    public void StoreItem(Item_ item)
    {
        if (IsInInventory(item))//Si el ítem ya está en alguna ranura del inventario
        {
            if (!IsStored(item))//Si no está en la ranura genérica
            {
                if (IsEquipped(item)) //Si está equipado
                    item.graphics_.inGame_.animator.SetTrigger("isStored");//Dejar de mostrarlo
                Slot_ oldSlot = item.occupyingSlot;
                oldSlot.RemoveItem(item);//Quitarlo de la ranura en que se encuentra actualmente
                genericSlot.AddItem(item);//Ponerlo en la ranura genérica ("almacenarlo")
            }
            else//Y si ya está en la ranura genérica ("almacenado")
            {
                Debug.Log($"Item {item.name} is already in the generic slot. It isn't necessary re-store it.");
            }
        }
        else//Y si no está en el inventario
        {
            float itemWeight = ((ItemStats_)item.mechanics_.stats_).weight;
            if ((currentWeight + itemWeight) > weightCapacity) return;//No equiparlo porque el jugador no puede llevar tanto peso
            genericSlot.AddItem(item);//Simplemente ponerlo en la ranura genérica ("almacenarlo")
            RefreshCurrentWeight();
            item.graphics_.inGame_.animator.SetTrigger("isStored");
        }
    }

    [Button]
    public void DropItem(Item_ item)
    {
        if (IsInInventory(item))//Si el ítem está en alguna ranura del inventario
        {
            Slot_ oldSlot = item.occupyingSlot;
            oldSlot.RemoveItem(item);
            RefreshCurrentWeight();
            item.graphics_.inGame_.animator.SetTrigger("isDropped");
        }
    }

    [Button]
    public void EquipItem(Item_ item)
    {
        if (IsInInventory(item))//Si el ítem ya está en alguna ranura del inventario
        {
            if (!IsEquipped(item))//Si no está equipado
            {
                Slot_ oldSlot = item.occupyingSlot;
                oldSlot.RemoveItem(item);//Quitarlo de la ranura en que se encuentra actualmente
                slots.Find(t => t.slotType == item.equippableSlot).AddItem(item);//Ponerlo en la ranura equipable
                item.graphics_.inGame_.animator.SetTrigger("isEquipped");//Equipar visualmente el ítem en el personaje
            }
            else//Si ya está equipado
            {
                Debug.Log($"Item {item.name} is already in the equippable slot. It isn't necessary re-equip it.");
            }
        }
        else//Y si no está en el inventario
        {
            if (currentWeight + ((ItemStats_)item.mechanics_.stats_).weight > weightCapacity) return;//No equiparlo porque el jugador no puede llevar tanto peso
            slots.Find(t => t.slotType == item.equippableSlot).AddItem(item);//Simplemente equiparlo
            item.graphics_.inGame_.animator.SetTrigger("isEquipped");//Equipar visualmente el ítem en el personaje
            SetWeightCapacity();
        }
    }

    [Button]
    public void UnequipItem(Item_ item)
    {
        if (IsEquipped(item))//Si el ítem está equipado
        {
            Slot_ oldSlot = item.occupyingSlot;
            oldSlot.RemoveItem(item);//Quitarlo de la ranura equipable
            genericSlot.AddItem(item);//Ponerlo en la ranura genérica ("almacenarlo")
            item.graphics_.inGame_.animator.SetTrigger("isStored");//Dejar de mostrarlo
        }
        else//Si el ítem no está equipado
        {
            Debug.Log($"Item {item.name} isn't equipped. It isn't necessary unequip it.");
        }
    }
    #endregion
}
