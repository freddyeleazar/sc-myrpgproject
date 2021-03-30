using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor.VersionControl;
#endif
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Custom/Dialogue")]
public class Dialogue : ScriptableObject
{
    [TextArea]
    public string dialogueText;

    [AssetList(Path = "Scriptables/Dialogues")]
    public List<Dialogue> dialogueInputs;

    [AssetList(Path = "Scriptables/Quests")]
    public List<Quest> requiredQuests;

    [AssetList(Path = "Scriptables/Quests")]
    public Quest activateQuest;
}