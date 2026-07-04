using UnityEngine;

public class Interaction : MonoBehaviour, IInteractable
{
    public GameObject panelTask;
    public void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
        panelTask.SetActive(true);
    }
}
