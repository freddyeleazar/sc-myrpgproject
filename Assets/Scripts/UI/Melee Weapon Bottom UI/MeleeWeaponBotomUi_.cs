using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponBotomUi_ : BaseBottomUi_
{
    private void Start()
    {
        leftContainer_.nameTextContainer.text.text = owner.name;
    }
}
