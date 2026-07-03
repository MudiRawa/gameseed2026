using UnityEngine;
using System.Collections;

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
            StartCoroutine(Selesai());
        }
    }

    IEnumerator Selesai()
    {
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
        SoundManager.Instance.PlaySFX(4);
    }
}