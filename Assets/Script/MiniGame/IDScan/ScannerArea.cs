using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ScannerArea : MonoBehaviour, IDropHandler
{
    public IDScanMinigame manager;

    private bool isScanning = false;

    public void OnDrop(PointerEventData eventData)
    {
        if (isScanning)
            return;

        DraggableIDCard card =
            eventData.pointerDrag.GetComponent<DraggableIDCard>();

        if (card != null)
        {
            if (manager.codeCorrect)
            {
                StartCoroutine(ScanRoutine(card));
            }
            else
            {
                Debug.Log("Code belum benar!");

                card.ReturnCard();
            }
        }
    }

    IEnumerator ScanRoutine(DraggableIDCard card)
    {
        isScanning = true;

        Debug.Log("SCANNING...");

        RectTransform rect =
            card.GetComponent<RectTransform>();

        rect.SetParent(transform);

        rect.anchoredPosition = Vector2.zero;

        // disable drag saat scanning
        card.enabled = false;

        // delay random 1-2 detik
        yield return new WaitForSeconds(
            Random.Range(1f, 2f)
        );

        Debug.Log("SCAN BERHASIL");

        manager.CompleteMinigame();

        isScanning = false;
    }
}