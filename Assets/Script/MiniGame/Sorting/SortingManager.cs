using UnityEngine;

public class SortingManager : MonoBehaviour
{
    public int totalCorrectNeeded = 6;

    private int currentCorrect = 0;

    public static SortingManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void AddCorrect()
    {
        currentCorrect++;

        if (currentCorrect >= totalCorrectNeeded)
        {
            Debug.Log("Minigame Selesai!");
            this.gameObject.SetActive(false);
        }
    }
}