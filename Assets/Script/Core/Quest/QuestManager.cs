using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    [Header("Quest List")]
    public Quest[] quests;

    private int currentQuestIndex = 0;

    private void Awake()
    {
        instance = this;
    }

    public Quest GetCurrentQuest()
    {
        if (currentQuestIndex >= quests.Length)
            return null;

        return quests[currentQuestIndex];
    }

    public bool IsCurrentQuest(int questIndex)
    {
        return currentQuestIndex == questIndex;
    }

    public void CompleteQuest()
    {
        if (currentQuestIndex >= quests.Length)
            return;

        quests[currentQuestIndex].isCompleted = true;

        Debug.Log(
            "Quest Completed: " +
            quests[currentQuestIndex].questName
        );

        currentQuestIndex++;

        if (currentQuestIndex < quests.Length)
        {
            Debug.Log(
                "Next Quest: " +
                quests[currentQuestIndex].questName
            );
        }
        else
        {
            Debug.Log("All quests completed!");
        }
    }
}