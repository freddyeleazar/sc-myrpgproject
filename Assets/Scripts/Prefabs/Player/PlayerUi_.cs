using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUi_ : UnitUi_
{
    public GameObject inventoryUiTemplate;
    public GameObject inventoryUi;

    public GameObject itemIconTemplate;

    public GameObject genericSlot;
    public GameObject headSlot;
    public GameObject bodySlot;
    public GameObject weaponSlot;
    public GameObject shieldSlot;
    public GameObject legsSlot;
    public GameObject footsSlot;

    public override void Awake()
    {
        base.Awake();
        Build();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryUi.activeSelf)
            {
                inventoryUi.SetActive(false);
            }
            else
            {
                inventoryUi.SetActive(true);
            }
        }
    }

    private void Build()
    {
        inventoryUi = Instantiate(inventoryUiTemplate, transform);
        inventoryUi.name = "Inventory UI";

        genericSlot = FindChild(inventoryUi, "Generic Slot");
        headSlot = FindChild(inventoryUi, "Head Slot");
        bodySlot = FindChild(inventoryUi, "Body Slot");
        weaponSlot = FindChild(inventoryUi, "Weapon Slot");
        shieldSlot = FindChild(inventoryUi, "Shield Slot");
        legsSlot = FindChild(inventoryUi, "Legs Slot");
        footsSlot = FindChild(inventoryUi, "Foots Slot");

        inventoryUi.SetActive(false);
    }

    public GameObject FindChild(GameObject parent, string childName)
    {
        GameObject child = parent.GetComponentsInChildren<RectTransform>().ToList().Find(t => t.name == childName).gameObject;
        return child;
    }
}
