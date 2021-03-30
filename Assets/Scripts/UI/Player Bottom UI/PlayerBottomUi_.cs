using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBottomUi_ : BaseBottomUi_
{
    public PlayerBottomUiMiddleContainer_ playerBottomUiMiddleContainer_;
    public PlayerBottomUiRightContainer_ playerBottomUiRightContainer_;

    private PlayerStats_ playerStats;

    private void Awake()
    {
        playerStats = (PlayerStats_)owner.mechanics_.stats_;
        playerBottomUiMiddleContainer_.healthBarContainer.slider.maxValue = playerStats.maxHp;
        playerBottomUiMiddleContainer_.healthBarContainer.slider.value = playerStats.maxHp;
        leftContainer_.nameTextContainer.text.text = playerStats.unitName;
    }

    private void Update()
    {
        playerBottomUiMiddleContainer_.healthBarContainer.slider.value = playerStats.hp;
    }
}
