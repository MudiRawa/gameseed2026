using UnityEngine;
using UnityEngine.EventSystems;

public class FolderSlot : MonoBehaviour, IDropHandler
{
    public FolderColor acceptedColor;
    private int folderCount = 0;
    private float folderSpacing = 10f;

    public void OnDrop(PointerEventData eventData)
    {
        DraggableFolder folder =
            eventData.pointerDrag.GetComponent<DraggableFolder>();

        if (folder != null)
        {
            if (folder.folderColor == acceptedColor)
            {
                folder.transform.SetParent(transform);

                RectTransform rect =
                    folder.GetComponent<RectTransform>();

                float yPosition = -folderCount * -folderSpacing;
                rect.anchoredPosition = new Vector2(0, (30 - yPosition));

                folder.LockFolder();
                folderCount++;

                Debug.Log("Benar!");
                SortingManager.instance.AddCorrect();
            }
            else
            {
                folder.ReturnToStart();

                Debug.Log("Salah!");
            }
        }
    }
}