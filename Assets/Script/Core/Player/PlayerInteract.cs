using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    private IInteractable currentInteractable;

    public void OnInteract(InputValue value)
    {
        if (!value.isPressed) return;

        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable =
            other.GetComponent<IInteractable>();

        if (interactable != null)
        {
            currentInteractable = interactable;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable interactable =
            other.GetComponent<IInteractable>();

        if (interactable != null &&
            interactable == currentInteractable)
        {
            currentInteractable = null;
        }
    }
}