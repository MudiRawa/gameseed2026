using UnityEngine;
using TMPro;
using System.Collections;

public class IDScanMinigame : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text displayText;
    public TMP_Text cardNumberText;

    [Header("Objects")]
    public GameObject idCard;

    private string generatedCode;
    private string playerInput = "";

    public bool codeCorrect = false;

    // LOCK KEYPAD
    private bool keypadLocked = false;

    private void Start()
    {
        idCard.SetActive(false);

        StartCoroutine(ShowCardRoutine());
    }

    IEnumerator ShowCardRoutine()
    {
        yield return new WaitForSeconds(Random.Range(1f, 2f));

        generatedCode = Random.Range(1000, 9999).ToString();

        cardNumberText.text = generatedCode;

        idCard.SetActive(true);
    }

    public void PressNumber(string number)
    {
        // kalau keypad terkunci
        if (keypadLocked)
            return;

        if (playerInput.Length >= 4)
            return;

        playerInput += number;

        displayText.text = playerInput;
    }

    public void ClearInput()
    {
        // kalau keypad terkunci
        if (keypadLocked)
            return;

        playerInput = "";

        displayText.text = "";
    }

    public void SubmitCode()
    {
        // kalau keypad terkunci
        if (keypadLocked)
            return;

        if (playerInput == generatedCode)
        {
            Debug.Log("Code Benar!");

            codeCorrect = true;

            // LOCK KEYPAD
            keypadLocked = true;

            displayText.text = "OK";
        }
        else
        {
            Debug.Log("Code Salah!");

            ClearInput();
        }
    }

    public void CompleteMinigame()
    {
        Debug.Log("TASK COMPLETE");
        this.gameObject.SetActive(false);
    }
}