using System.Collections.Generic;
using UnityEngine;

public class PanelSubject : MonoBehaviour, ISubject
{
    private List<IObserver> promptObservers = new List<IObserver>();
    private List<IObserver> interactObservers = new List<IObserver>();

    public InteractPrompt prompt;

    private void Start()
    {
        // 1️⃣ Add the prompt observer if assigned
        if (prompt != null)
            AddPromptObserver(prompt);

        // 2️⃣ Auto-register all IObservers on this object except the prompt
        foreach (var observer in GetComponents<IObserver>())
        {
            if (observer != prompt)
                AddInteractObserver(observer);
        }
    }

    public void AddPromptObserver(IObserver observer) => promptObservers.Add(observer);
    public void AddInteractObserver(IObserver observer) => interactObservers.Add(observer);
    public void RemovePromptObserver(IObserver observer) => promptObservers.Remove(observer);
    public void RemoveInteractObserver(IObserver observer) => interactObservers.Remove(observer);

    public void NotifyPromptObservers()
    {
        foreach (var observer in promptObservers)
            observer.OnNotify();
    }

    public void NotifyInteractObservers()
    {
        foreach (var observer in interactObservers)
            observer.OnNotify();
    }

    // Trigger detection
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        InteractionObserver player = collision.GetComponent<InteractionObserver>();
        if (player != null)
            player.SetInteractable(this);

        NotifyPromptObservers(); // show prompt
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        InteractionObserver player = collision.GetComponent<InteractionObserver>();
        if (player != null)
            player.ClearInteractable();

        prompt?.HidePrompt();
    }
}