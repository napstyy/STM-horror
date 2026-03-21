using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueData dialogueData;
    [SerializeField] private string playerTag = "Player";

    private bool playerInRange;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
            playerInRange = false;
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.Instance.IsOpen && Keyboard.current.eKey.wasPressedThisFrame)
            DialogueManager.Instance.StartDialogue(dialogueData);
    }
}