using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCommunication_ : UnitCommunication_
{
    [AssetList(Path = "Scriptables/Dialogues/Player")]
    public List<Dialogue> dialogueRepertoire;

    public List<Dialogue> GetPlayerAnswers(Dialogue npcQuestion)
    {
        List<Dialogue> playerAnswers = dialogueRepertoire.FindAll(dialogue => dialogue.dialogueInputs.Contains(npcQuestion)).FindAll(dialogue => dialogue.requiredQuests.Contains(questManager.GetActiveQuest()));
        return playerAnswers;
    }

    public Dialogue GetGreetingDialogue()
    {
        return dialogueRepertoire.Find(t => t.name == "Saludar (Genérico)");
    }
}
