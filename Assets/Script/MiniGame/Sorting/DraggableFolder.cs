using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableFolder : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    public FolderColor folderColor;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;

    private Vector2 startPosition;
    private bool isPlaced = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isPlaced) return;
        
        startPosition = rectTransform.anchoredPosition;

        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isPlaced) return;
        
        Vector2 position;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out position
        );

        rectTransform.anchoredPosition = position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isPlaced) return;
        
        canvasGroup.blocksRaycasts = true;
    }

    public void ReturnToStart()
    {
        rectTransform.anchoredPosition = startPosition;
    }

    public void LockFolder()
    {
        isPlaced = true;
        canvasGroup.interactable = false;
    }
}