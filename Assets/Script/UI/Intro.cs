using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour
{
    public GameObject introPanel;

    private void Start()
    {
        introPanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game
        StartCoroutine(WaitAndHideIntro());
    }

    IEnumerator WaitAndHideIntro()
    {
        yield return new WaitForSecondsRealtime(1.5f); // Wait for 1.5 seconds in real time
        introPanel.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }
}

