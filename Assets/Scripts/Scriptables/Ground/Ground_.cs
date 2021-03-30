using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "New Ground", menuName = "Custom/Ground")]
public class Ground_ : TileOwner_
{
    public BiomeType biomeType;

    public GroundGraphics_ graphics_;
    public GroundMechanics_ mechanics_;
    public GroundInventory_ inventory_;
}