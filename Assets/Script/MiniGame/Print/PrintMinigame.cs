using UnityEngine;
using System.Collections;

public class PrintMinigame : MonoBehaviour
{
    public Animator printAnimator;
    public Animator paperAnimator;

    public void StartPrinting()
    {
        Debug.Log("Start Printing");
        StartCoroutine(PrintingCoroutine());
    }
    void CompleteMinigame()
    {
        Debug.Log("TASK COMPLETE");
        this.gameObject.SetActive(false);
        SoundManager.Instance.PlaySFX(4);
    }

    IEnumerator PrintingCoroutine()
    {
        printAnimator.SetBool("Print", true);
        paperAnimator.SetTrigger("Print");
        yield return new WaitForSeconds(16f);
        printAnimator.SetBool("Print", false);

        yield return new WaitForSeconds(1f);
        CompleteMinigame();
    }
}
