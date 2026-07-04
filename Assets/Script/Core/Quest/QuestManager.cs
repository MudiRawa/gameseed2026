using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    [Header("Quest List")]
    public Quest[] quests;

    [Header("Quest UI")]
    public TextMeshProUGUI[] questTexts;

    private void Awake()
    {
        instance = this;
    }

    public bool IsQuestCompleted(int questIndex)
    {
        if (questIndex < 0 || questIndex >= quests.Length)
            return false;

        return quests[questIndex].isCompleted;
    }

    public void CompleteQuest(int questIndex)
    {
        if (questIndex < 0 || questIndex >= quests.Length)
            return;

        if (quests[questIndex].isCompleted)
            return;

        quests[questIndex].isCompleted = true;

        if (questTexts.Length > questIndex)
        {
            questTexts[questIndex].text =
                "<s>" + questTexts[questIndex].text + "</s>";
        }

        Debug.Log(
            "Quest Completed: " +
            quests[questIndex].questName
        );
    }
}