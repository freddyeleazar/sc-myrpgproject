using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Slot_: MonoBehaviour
{
    public Owner_ owner;

    [OnValueChanged("SetSlotNameAndUI")]
    public SlotType slotType;
    public string slotName;
    public bool isOccupied;
    public List<Item_> storedItems;
    public float currentWeight;

    public PlayerUi_ playerUi;
    public GameObject slotUI;

    private void Start()
    {
        SetSlotNameAndUI();
    }

    public Slot_ CreateSlot(SlotType slotType, Owner_ owner)
    {
        this.owner = owner;
        SetPlayerUI();
        this.slotType = slotType;
        SetSlotNameAndUI();
        storedItems = new List<Item_>();
        return this;
    }

    public void SetPlayerUI()
    {
        if (IsPlayer())
        {
            Player_ player = (Player_)owner;
            playerUi = player.GetComponentInChildren<PlayerUi_>();
        }
    }

    public bool IsPlayer()
    {
        return owner.GetType() == typeof(Player_);
    }

    public void SetSlotNameAndUI()
    {
        switch (slotType)
        {
            case SlotType.Abdomen:
                slotName = "Abdomen";
                if (IsPlayer()) slotUI = playerUi.bodySlot;
                break;
            case SlotType.Arms:
                slotName = "Brazos";
                if (IsPlayer()) slotUI = playerUi.shieldSlot;
                break;
            case SlotType.BaseBody:
                slotName = "Cuerpo base";
                if (IsPlayer()) slotUI = playerUi.bodySlot;
                break;
            case SlotType.Breastplate:
                slotName = "Coraza";
                if (IsPlayer()) slotUI = playerUi.bodySlot;
                break;
            case SlotType.Cape:
                slotName = "Capa";
                if (IsPlayer()) slotUI = playerUi.bodySlot;
                break;
            case SlotType.FacialHair:
                slotName = "Vello facial";
                if (IsPlayer()) slotUI = playerUi.headSlot;
                break;
            case SlotType.Feet:
                slotName = "Pies";
                if (IsPlayer()) slotUI = playerUi.footsSlot;
                break;
            case SlotType.Forearms:
                slotName = "Antebrazos";
                if (IsPlayer()) slotUI = playerUi.shieldSlot;
                break;
            case SlotType.Front:
                slotName = "Pectoral";
                if (IsPlayer()) slotUI = playerUi.bodySlot;
                break;
            case SlotType.Baggage:
                slotName = "Bagaje";
                if (IsPlayer())
                {
                    slotUI = playerUi.genericSlot;
                }
                break;
            case SlotType.Head:
                slotName = "Cabeza";
                if (IsPlayer()) slotUI = playerUi.headSlot;
                break;
            case SlotType.Kneepads:
                slotName = "Rodilleras";
                if (IsPlayer()) slotUI = playerUi.legsSlot;
                break;
            case SlotType.LeftArm:
                slotName = "Brazo izquierdo";
                if (IsPlayer()) slotUI = playerUi.shieldSlot;
                break;
            case SlotType.Neck:
                slotName = "Cuello";
                if (IsPlayer()) slotUI = playerUi.headSlot;
                break;
            case SlotType.Pants:
                slotName = "Pantalones";
                if (IsPlayer()) slotUI = playerUi.legsSlot;
                break;
            case SlotType.RightArm:
                slotName = "Brazo derecho";
                if (IsPlayer()) slotUI = playerUi.weaponSlot;
                break;
            case SlotType.ShoulderPads:
                slotName = "Hombreras";
                if (IsPlayer()) slotUI = playerUi.bodySlot;
                break;
            case SlotType.Skirt:
                slotName = "Falda";
                if (IsPlayer()) slotUI = playerUi.legsSlot;
                break;
            case SlotType.Tunica:
                slotName = "Túnica";
                if (IsPlayer()) slotUI = playerUi.bodySlot;
                break;
            case SlotType.Vest:
                slotName = "Chaleco";
                if (IsPlayer()) slotUI = playerUi.bodySlot;
                break;
            default:
                break;
        }
    }

    public void AddItem(Item_ item)
    {
        storedItems.Add(item);
        item.transform.parent = transform;
        item.transform.localPosition = Vector3.zero;
        isOccupied = true;
        item.occupyingSlot = this;
        
        //UI Actions
        GameObject itemIcon = ((ItemUi_)item.graphics_.ui).icon;
        itemIcon.name = item.name;
        if(IsPlayer()) Instantiate(itemIcon, slotUI.transform);
    }

    public void RemoveItem(Item_ item)
    {
        storedItems.Remove(item);
        item.transform.parent = null;
        isOccupied = false;
        item.occupyingSlot = null;

        //UI Actions
        if (IsPlayer())
        {
            GameObject itemIcon = slotUI.GetComponentsInChildren<Image>().ToList().Find(t => t.name == item.name + "(Clone)").gameObject;
            DestroyImmediate(itemIcon, true);
        }
    }

    public void ChangeItemSlot(Item_ item, Slot_ newSlot)
    {
        RemoveItem(item);
        newSlot.AddItem(item);
    }

}

public enum SlotType
{
    Abdomen,
    Arms,
    BaseBody,
    Breastplate,
    Cape,
    FacialHair,
    Feet,
    Forearms,
    Front,
    Baggage,
    Head,
    Kneepads,
    LeftArm,
    Neck,
    Pants,
    RightArm,
    ShoulderPads,
    Skirt,
    Tunica,
    Vest
}