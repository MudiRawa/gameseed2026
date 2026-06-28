using UnityEngine;

public class Desk : MonoBehaviour, IInteractable
{
    public int questIndex;

    public void Interact()
    {
        if (!QuestManager.instance.IsCurrentQuest(questIndex))
        {
            Debug.Log(QuestManager.instance.GetCurrentQuest().description);
            return;
        }

        //do something here before complete

        Debug.Log("Complete");

        QuestManager.instance.CompleteQuest();
    }
}