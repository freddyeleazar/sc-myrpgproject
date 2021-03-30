using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NpcBottomUiRightContainer_ : BaseBottomUiRightContainer_
{
    public InputField textInput;

    public GameObject playerDialogueButtonsContainer;
    public GameObject dialogueLogContainer;

    public Button buttonTemplate;
    public Text textTemplate;

    public PlayerCommunication_ playerCommunication;
    public NpcCommunication_ npcCommunication;
    public QuestManager questManager;

    public bool isStarted;

    private void Start()
    {
        playerDialogueButtonsContainer = GetComponentsInChildren<ScrollRect>().ToList().Find(t => t.gameObject.name == "Player Dialogue Buttons Container").gameObject.GetComponentInChildren<ContentSizeFitter>().gameObject;
        dialogueLogContainer = GetComponentsInChildren<ScrollRect>().ToList().Find(t => t.gameObject.name == "Dialogue Log Container").gameObject.GetComponentInChildren<ContentSizeFitter>().gameObject;

        playerCommunication = FindObjectOfType<PlayerCommunication_>();
        npcCommunication = (NpcCommunication_)((NpcBehaviour_)((Npc_)GetComponentInParent<NpcBottomUi_>().owner).mechanics_.behaviour_).unitCommunication_;
        NpcStats_ thisNpc = (NpcStats_)((Npc_)GetComponentInParent<NpcBottomUi_>().owner).mechanics_.stats_;
        questManager = FindObjectOfType<QuestManager>();

        if (!isStarted && thisNpc.diplomacy == Diplomacy.Allied)
        {
            Button greetingButton = Instantiate(buttonTemplate, playerDialogueButtonsContainer.transform);
            greetingButton.GetComponentInChildren<Text>().text = playerCommunication.GetGreetingDialogue().dialogueText;
            greetingButton.onClick.AddListener
                (
                    delegate
                    {
                        questManager.ActivateQuest(playerCommunication.GetGreetingDialogue().activateQuest);
                        RefreshNpcDialogueLog(playerCommunication.GetGreetingDialogue());
                    }
                );
            isStarted = true;
        }
    }

    public void RefreshPlayerDialogueButtons(Dialogue npcQuestion)
    {
        DestroyPreviousPlayerDialogueButtons();
        List<Dialogue> playerAnswers = playerCommunication.GetPlayerAnswers(npcQuestion);
        foreach (Dialogue playerAnswer in playerAnswers)
        {
            Button dialogueButton = Instantiate(buttonTemplate, playerDialogueButtonsContainer.transform);
            dialogueButton.GetComponentInChildren<Text>().text = playerAnswer.dialogueText;
            dialogueButton.onClick.AddListener
                (
                    delegate
                    {
                        questManager.ActivateQuest(playerAnswer.activateQuest);
                        RefreshNpcDialogueLog(playerAnswer);
                    }
                );
        }
    }

    public void RefreshNpcDialogueLog(Dialogue playerQuestion)
    {
        Dialogue npcAnswer = npcCommunication.GetNpcAnswer(playerQuestion);
        Text dialogueText = Instantiate(textTemplate, dialogueLogContainer.transform);
        dialogueText.text = npcAnswer.dialogueText;
        RefreshPlayerDialogueButtons(npcAnswer);
    }

    public void DestroyPreviousPlayerDialogueButtons()
    {
        playerDialogueButtonsContainer.gameObject.GetComponentsInChildren<Button>().ToList().ForEach(t => DestroyImmediate(t.gameObject, true));
    }
}
