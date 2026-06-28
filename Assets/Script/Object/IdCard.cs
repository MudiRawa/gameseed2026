using UnityEngine;

public class IdCard : MonoBehaviour, IInteractable
{
    public int questIndex;

    public void Interact()
    {
        if (!QuestManager.instance.IsCurrentQuest(questIndex))
        {
            Debug.Log("Quest belum aktif");
            return;
        }

        Debug.Log("Player mengambil ID Card");

        //do something before complete
        QuestManager.instance.CompleteQuest();
        Destroy(gameObject);
    }
}