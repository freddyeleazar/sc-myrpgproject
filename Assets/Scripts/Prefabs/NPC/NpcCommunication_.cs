using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCommunication_ : UnitCommunication_
{
    [AssetList(Path = "Scriptables/Dialogues/NPC")]
    public List<Dialogue> dialogueRepertoire;

    public GameObject backup;

    public Dialogue GetNpcAnswer(Dialogue playerQuestion)
    {
        Dialogue npcAnswer = dialogueRepertoire.FindAll(dialogue => dialogue.dialogueInputs.Contains(playerQuestion)).Find(dialogue => dialogue.requiredQuests.Contains(questManager.GetActiveQuest()) || dialogue.requiredQuests == null);
        return npcAnswer;
    }
}
