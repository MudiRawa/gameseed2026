using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;

public class TypingMinigame : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text typingText;

    [Header("Send Button")]
    public Button sendButton;
    public CanvasGroup sendCanvasGroup;

    [Header("Settings")]
    [TextArea(3, 5)]
    public string targetWord =
        "I am holding the files in my hand.";

    private int currentIndex = 0;

    private bool isFlashingRed = false;

    public int questIndex;

    private void Start()
    {
        UpdateVisualText();

        sendButton.interactable = false;

        sendCanvasGroup.alpha = 0.5f;
    }

    private void OnEnable()
    {
        Keyboard.current.onTextInput += HandleCharacterInput;
    }

    private void OnDisable()
    {
        if (Keyboard.current != null)
        {
            Keyboard.current.onTextInput -= HandleCharacterInput;
        }
    }

    void HandleCharacterInput(char typedChar)
    {
        if (currentIndex >= targetWord.Length)
            return;

        char correctChar = targetWord[currentIndex];

        // BENAR
        if (typedChar == correctChar)
        {
            currentIndex++;

            UpdateVisualText();

            CheckCompletion();
        }
        else
        {
            StartCoroutine(FlashRed());
        }
    }

    void UpdateVisualText()
    {
        string result = "";

        for (int i = 0; i < targetWord.Length; i++)
        {
            char c = targetWord[i];

            // SUDAH DIKETIK
            if (i < currentIndex)
            {
                if (isFlashingRed)
                {
                    result +=
                        $"<color=#FF4444>{c}</color>";
                }
                else
                {
                    result +=
                        $"<color=#798F9B>{c}</color>";
                }
            }
            else
            {
                // transparan
                result +=
                    $"<color=#00000085>{c}</color>";
            }
        }

        typingText.text = result;
    }
    void CheckCompletion()
    {
        if (currentIndex >= targetWord.Length)
        {
            sendButton.interactable = true;

            sendCanvasGroup.alpha = 1f;

            Debug.Log("Typing Complete!");
        }
    }

    IEnumerator FlashRed()
    {
        isFlashingRed = true;

        UpdateVisualText();

        yield return new WaitForSeconds(0.15f);

        isFlashingRed = false;

        UpdateVisualText();
    }

    public void Send()
    {
        Debug.Log("MESSAGE SENT");
        StartCoroutine(Selesai());
    }

    IEnumerator Selesai()
    {
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
        SoundManager.Instance.PlaySFX(4);
        QuestManager.instance.CompleteQuest(questIndex);
    }
}