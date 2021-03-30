using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemUi_ : Ui_
{
    public GameObject icon;

    private void Start()
    {
        icon.GetComponent<ItemIcon>().owner = owner;
        bottomUI.GetComponent<BaseBottomUi_>().owner = (Base_)owner;
        MeleeWeaponBotomUi_ itemUi = bottomUI.GetComponent<MeleeWeaponBotomUi_>();
        Sprite iconSprite = icon.GetComponentsInChildren<Image>().ToList().Find(t => t.gameObject.name == "Image").GetComponent<Image>().sprite;
        itemUi.leftContainer_.avatarContainer.background_.color = Color.black;
        itemUi.leftContainer_.avatarContainer.image_.sprite = iconSprite;
    }
}
