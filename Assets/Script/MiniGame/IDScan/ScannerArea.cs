using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScannerArea : MonoBehaviour
{
    [Header("References")]
    public IDScanMinigame manager;

    [Header("Scanner Visual")]
    public Image scanSprite;

    public Sprite idleSprite;
    public Sprite scanningSprite;
    public Sprite successSprite;

    [Header("Scan Settings")]
    public float scanDuration = 2f;

    private bool isScanning = false;

    private RectTransform scannerRect;

    private DraggableIDCard currentCard;

    public int questIndex;

    private void Start()
    {
        scannerRect = GetComponent<RectTransform>();

        scanSprite.sprite = idleSprite;
    }

    private void Update()
    {
        // kalau belum input code benar
        if (!manager.codeCorrect)
            return;

        // cari kartu
        DraggableIDCard card =
            FindObjectOfType<DraggableIDCard>();

        if (card == null)
            return;

        // cek overlap
        bool overlapping =
            RectOverlaps(
                scannerRect,
                card.GetComponent<RectTransform>()
            );

        // mulai scan
        if (overlapping && card.isDragging && !isScanning)
        {
            StartCoroutine(ScanRoutine(card));
        }
    }

    bool RectOverlaps(RectTransform a, RectTransform b)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(
            a,
            b.position
        );
    }

    IEnumerator ScanRoutine(DraggableIDCard card)
    {
        isScanning = true;

        currentCard = card;

        Debug.Log("SCAN START");

        scanSprite.sprite = scanningSprite;

        float timer = 0f;

        while (timer < scanDuration)
        {
            // kalau kartu keluar area
            if (!RectOverlaps(
                scannerRect,
                card.GetComponent<RectTransform>()))
            {
                Debug.Log("SCAN FAILED");

                scanSprite.sprite = idleSprite;

                isScanning = false;

                yield break;
            }

            timer += Time.deltaTime;

            yield return null;
        }

        // sukses
        Debug.Log("SCAN SUCCESS");

        scanSprite.sprite = successSprite;

        yield return new WaitForSeconds(1f);

        manager.CompleteMinigame();
        QuestManager.instance.CompleteQuest(questIndex);

        isScanning = false;
    }
}