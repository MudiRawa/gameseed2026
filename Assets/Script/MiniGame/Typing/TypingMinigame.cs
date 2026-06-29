using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class TypingMinigame : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text targetWordText;
    public TMP_Text outputText;

    [Header("Send Button")]
    public Button sendButton;
    public CanvasGroup sendCanvasGroup;

    [Header("Settings")]
    public string targetWord = "APEL";

    // mapping huruf
    private Dictionary<char, char> letterMap =
        new Dictionary<char, char>();

    // index huruf saat ini
    private int currentIndex = 0;

    // hasil output random
    private string outputString = "";

    private void Start()
    {
        // uppercase semua
        targetWord = targetWord.ToUpper();

        // tampilkan target word
        targetWordText.text = targetWord;

        // generate random mapping
        GenerateLetterMap();

        // kosongkan output
        outputText.text = "";

        // disable tombol send
        sendButton.interactable = false;

        // opacity tombol send
        sendCanvasGroup.alpha = 0.5f;
    }

    void GenerateLetterMap()
    {
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        foreach (char c in targetWord)
        {
            if (!letterMap.ContainsKey(c))
            {
                char randomChar =
                    alphabet[Random.Range(0, alphabet.Length)];

                letterMap.Add(c, randomChar);
            }
        }
    }

    private void Update()
    {
        Keyboard kb = Keyboard.current;

        if (kb == null)
            return;

        // cek semua huruf A-Z
        for (char c = 'A'; c <= 'Z'; c++)
        {
            Key key = (Key)System.Enum.Parse(
                typeof(Key),
                c.ToString()
            );

            if (kb[key].wasPressedThisFrame)
            {
                HandleTyping(c);
            }
        }
    }

    void HandleTyping(char typedChar)
    {
        // kalau sudah selesai
        if (currentIndex >= targetWord.Length)
            return;

        // huruf yang benar
        char correctChar = targetWord[currentIndex];

        // kalau benar
        if (typedChar == correctChar)
        {
            // ambil random mapped char
            char mappedChar = letterMap[correctChar];

            // tambahkan ke output
            outputString += mappedChar;

            // update text
            outputText.text = outputString;

            // lanjut index
            currentIndex++;

            // cek selesai
            CheckCompletion();
        }
        else
        {
            // salah -> flash merah
            StartCoroutine(FlashRed());
        }
    }

    void CheckCompletion()
    {
        // kalau selesai
        if (currentIndex >= targetWord.Length)
        {
            // enable send
            sendButton.interactable = true;

            // opacity normal
            sendCanvasGroup.alpha = 1f;

            Debug.Log("Typing Complete!");
        }
    }

    IEnumerator FlashRed()
    {
        Color originalColor = outputText.color;

        outputText.color = Color.red;

        yield return new WaitForSeconds(0.15f);

        outputText.color = originalColor;
    }

    public void Send()
    {
        Debug.Log("MESSAGE SENT");
        this.gameObject.SetActive(false);
    }
}