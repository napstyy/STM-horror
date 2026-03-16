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
        if (sprite != null)
        {
            sprite.enabled = true;
        }
    }

    public void HidePrompt()
    {
        if (sprite != null)
        {
            sprite.enabled = false;
        }
    }
    
    
}