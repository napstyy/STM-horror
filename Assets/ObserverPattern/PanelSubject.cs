using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PanelSubject : MonoBehaviour, ISubject
{
    private List<IObserver> observers = new List<IObserver>();
    public InteractPrompt prompt;
    private bool playerInRange = false;
    
     
    private void Start()
    {
        AddObserver(prompt);
    }
    public void AddObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.OnNotify();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            NotifyObservers(); // show prompt
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            prompt.HidePrompt();
        }
    }
}