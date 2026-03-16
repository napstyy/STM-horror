using UnityEngine;

public class ColorObserver : MonoBehaviour, IObserver
{
    public PanelSubject subject;

    private SpriteRenderer rend;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();

        // Subscribe to the interact event
        if(subject != null)
        {
            subject.AddInteractObserver(this);
        }
        
    }

    public void OnNotify()
    {
        if (rend != null)
        {
            rend.material.SetColor("_TargetColor", new Color(34f / 255f, 185f / 255f, 42f / 255f, 1f));
        }
    }
}