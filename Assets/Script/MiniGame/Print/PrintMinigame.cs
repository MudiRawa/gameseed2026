using UnityEngine;
using System.Collections;

public class PrintMinigame : MonoBehaviour
{
    public void StartPrinting()
    {
        Debug.Log("Start Printing");
        StartCoroutine(PrintingCoroutine());
    }
    void CompleteMinigame()
    {
        Debug.Log("TASK COMPLETE");
        this.gameObject.SetActive(false);
    }

    IEnumerator PrintingCoroutine()
    {
        yield return new WaitForSeconds(5f);

        CompleteMinigame();
    }
}
