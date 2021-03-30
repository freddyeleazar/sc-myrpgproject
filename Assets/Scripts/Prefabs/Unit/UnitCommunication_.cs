using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UnitCommunication_ : Component_
{
    public QuestManager questManager;

    public void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
    }
}
