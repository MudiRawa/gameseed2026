using UnityEngine;
using UnityEngine.EventSystems;

public class FolderSlot : MonoBehaviour, IDropHandler
{
    public FolderColor acceptedColor;

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

                rect.anchoredPosition = Vector2.zero;

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