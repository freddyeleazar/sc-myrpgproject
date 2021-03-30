using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GroundGenerator_ : MonoBehaviour
{
    [AssetList(Path = "Tiles/Grounds")]
    public List<Ground_> grounds = new List<Ground_>();

    [Button]
    public Ground_ GenerateGround(float temperature, float moisture)
    {
        List<Ground_> groundsToRandomGeneration = new List<Ground_>();
        foreach (Ground_ ground in grounds)
        {
            ground.Build();
            int generationChances = GetGenerationChances(ground, temperature, moisture);
            for (int i = 1; i < generationChances; i++)
            {
                groundsToRandomGeneration.Add(ground);
            }
        }

        if(groundsToRandomGeneration.Count > 0)
        {
            Ground_ randomGround = groundsToRandomGeneration[Random.Range(0, groundsToRandomGeneration.Count)];
            randomGround.sprite = randomGround.graphics_.ingame_.sprites[Random.Range(0, randomGround.graphics_.ingame_.sprites.Count)];
            
            return randomGround;
        }
        else
        {
            return null;
        }

    }

    [Button]
    public int GetGenerationChances(Ground_ ground, float temperature, float moisture)
    {
        int generationChances = 0;
        GroundStats_ groundStats = ground.mechanics_.stats_;

        if(groundStats.biome.IsSupported(temperature, moisture))
        {
            generationChances = groundStats.biome.GetGenerationChances(temperature, moisture);
        }
        return generationChances;
    }
}