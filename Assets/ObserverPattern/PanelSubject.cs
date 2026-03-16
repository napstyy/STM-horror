using System.Collections.Generic;
using UnityEngine;

public class PanelSubject : MonoBehaviour, ISubject
{
    private List<IObserver> promptObservers = new List<IObserver>();
    private List<IObserver> interactObservers = new List<IObserver>();

    public InteractPrompt prompt;

    private void Start()
    {
        if (prompt != null)
        {
            AddPromptObserver(prompt);
        }
    }

    public void AddPromptObserver(IObserver observer)
    {
        promptObservers.Add(observer);
    }

    public void AddInteractObserver(IObserver observer)
    {
        interactObservers.Add(observer);
    }

    public void RemovePromptObserver(IObserver observer)
    {
        promptObservers.Remove(observer);
    }

    public void RemoveInteractObserver(IObserver observer)
    {
        interactObservers.Remove(observer);
    }

    public void NotifyPromptObservers()
    {
        foreach (var observer in promptObservers)
        {
            observer.OnNotify();
        }
    }

    public void NotifyInteractObservers()
    {
        foreach (var observer in interactObservers)
        {
            observer.OnNotify();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MovementScript player = collision.GetComponent<MovementScript>();

            if (player != null)
            {
                player.SetInteractable(this);
            }

            NotifyPromptObservers(); // show the "E" prompt
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MovementScript player = collision.GetComponent<MovementScript>();

            if (player != null)
            {
                player.ClearInteractable();
            }

            if (prompt != null)
            {
                prompt.HidePrompt();
            }
        }
    }
}