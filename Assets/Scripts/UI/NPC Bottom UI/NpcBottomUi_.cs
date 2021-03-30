using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcBottomUi_ : BaseBottomUi_
{
    private void Start()
    {
        ((NpcBottomUiLeftContainer_)leftContainer_).barContainers.hpBar.maxValue = ((NpcStats_)owner.mechanics_.stats_).maxHp;
        ((NpcBottomUiLeftContainer_)leftContainer_).barContainers.hpBar.value = ((NpcStats_)owner.mechanics_.stats_).maxHp;
        leftContainer_.nameTextContainer.text.text = ((NpcStats_)owner.mechanics_.stats_).unitName;
    }

    private void Update()
    {
        ((NpcBottomUiLeftContainer_)leftContainer_).barContainers.hpBar.value = ((NpcStats_)owner.mechanics_.stats_).hp;

    }
}
