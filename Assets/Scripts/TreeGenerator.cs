using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TreeGenerator : SerializedMonoBehaviour
{
    [AssetList(Path = "Prefabs/Trees")]
    public List<Tree_> trees;
    
    [Button]
    public Tree_ GenerateTree(float temperature, float moisture)
    {
        List<Tree_> treesToRandomGeneration = new List<Tree_>();
        foreach (Tree_ tree in trees)
        {
            tree.Build();
            int generationChances = GetGenerationChances(tree, temperature, moisture);
            for (int i = 1; i < generationChances; i++)
            {
                treesToRandomGeneration.Add(tree);
            }
        }
        if(treesToRandomGeneration.Count > 0)
        {
            Tree_ tree = treesToRandomGeneration[Random.Range(0, treesToRandomGeneration.Count)];
            return tree;
        }
        else
        {
            return null;
        }
    }

    [Button]
    public int GetGenerationChances(Tree_ tree, float temperature, float moisture)
    {
        int generationChances = 0;
        TreeStats_ treeStats = (TreeStats_)tree.mechanics_.stats_;

        if(treeStats.biome.IsSupported(temperature, moisture))
        {
            generationChances = treeStats.biome.GetGenerationChances(temperature, moisture);
        }
        return generationChances;
    }
}