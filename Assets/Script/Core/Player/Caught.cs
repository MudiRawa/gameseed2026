using UnityEngine;
using System.Collections;

public class Caught : MonoBehaviour
{
    public GameObject caughtUI;
    public bool isCaught = false;
    public Transform StartPosition;

    public static Caught instance;

    void Awake()
    {
        instance = this;
        StartPosition = transform;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("NPC") && !isCaught)
        {
            Debug.Log("Player Caught!");
            StartCoroutine(WaitAndHideUI());
        }
    }

    IEnumerator WaitAndHideUI()
    {
        isCaught = true;
        caughtUI.SetActive(true);
        yield return new WaitForSeconds(1f);
        PlayerMovement.instance.ResetPlayer();
        NPCController.instance.ResetNPC();
        yield return new WaitForSeconds(1f);
        caughtUI.SetActive(false);
        isCaught = false;
    }
}
