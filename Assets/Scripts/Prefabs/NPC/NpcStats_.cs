using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcStats_ : UnitStats_
{
    public int aggressiveness;

    public int GetAttackThreshold()
    {
        return 100 - aggressiveness;
    }

    public int GetRunAwayThreshold()
    {
        return 100 - GetAttackThreshold();
    }
}
