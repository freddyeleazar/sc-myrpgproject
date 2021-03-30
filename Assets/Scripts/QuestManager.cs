using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{
    [AssetList(Path = "Scriptables/Quests")]
    public List<Quest> quests;

    public UnitStats_ samStats;
    public PlayerStats_ playerStats;
    public UnitStats_ granjeroStats;

    public List<Npc_> bandits;

    private void Start()
    {
        DeactivateAll();
        quests.Find(quest => quest.name == "Rescate de Sam 00 (Unos granjeros preocupados)").isActive = true;
    }

    private void Update()
    {
        if(samStats.hp < 0)
        {
            SceneManager.LoadScene(3);
        }
        if(playerStats.hp < 0)
        {
            SceneManager.LoadScene(2);
        }
        if(granjeroStats.hp < 0)
        {
            SceneManager.LoadScene(4);
        }

        if (bandits.FindAll(t => ((NpcStats_)t.mechanics_.stats_).hp <= 0).Count == bandits.Count && GetActiveQuest().name != "Rescate de Sam 03 (Escoltar a Sam)" && GetActiveQuest().name != "Fin")
        {
            ActivateQuest(quests.Find(t => t.name == "Rescate de Sam 03 (Escoltar a Sam)"));
            NpcBehaviour_ samBehaviour = (NpcBehaviour_)((Npc_)samStats.owner).mechanics_.behaviour_;
        }

        if (GetActiveQuest().name == "Rescate de Sam 03 (Escoltar a Sam)")
        {
            NpcBehaviour_ samBehaviour = (NpcBehaviour_)((Npc_)samStats.owner).mechanics_.behaviour_;
            samBehaviour.target = playerStats.transform.position + new Vector3(1, 1, 1);
        }

        if(GetActiveQuest().name == "Fin")
        {
            Debug.Log(GetActiveQuest().name);
            SceneManager.LoadScene(5);
        }

    }

    public Quest GetActiveQuest()
    {
        return quests.Find(quest => quest.isActive);
    }

    public void ActivateQuest(Quest quest)
    {
        if(GetQuest(quest) != null )
        {
            DeactivateAll();
            GetQuest(quest).isActive = true;
        }
    }

    public Quest GetQuest(Quest quest)
    {
        return quests.Find(t => t == quest);
    }

    public void DeactivateAll()
    {
        quests.ForEach(t => t.isActive = false);
    }
}
