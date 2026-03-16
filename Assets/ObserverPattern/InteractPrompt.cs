using UnityEngine;

public class InteractPrompt : MonoBehaviour, IObserver
{
    public SpriteRenderer sprite;

    private void Start()
    {
        
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
    }

    public void OnNotify()
    {
        sprite.enabled = true;
    }

    public void HidePrompt()
    {
        sprite.enabled = false;
    }
    
    
}