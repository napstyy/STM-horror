using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionObserver : MonoBehaviour
{
    private PanelSubject currentInteractable;

    // Called by objects when the player enters trigger
    public void SetInteractable(PanelSubject panel)
    {
        currentInteractable = panel;
    }

    public void ClearInteractable()
    {
        currentInteractable = null;
    }

    // Called by Input System when E is pressed
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && currentInteractable != null)
        {
            // Notify this object's observers
            currentInteractable.NotifyInteractObservers();
        }
    }
}   