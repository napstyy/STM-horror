using UnityEngine;

public interface ISubject
{
    void AddPromptObserver(IObserver observer);
    void RemovePromptObserver(IObserver observer);

    // Interact observers (press E / change color)
    void AddInteractObserver(IObserver observer);
    void RemoveInteractObserver(IObserver observer);

    // Notify functions
    void NotifyPromptObservers();
    void NotifyInteractObservers();
}