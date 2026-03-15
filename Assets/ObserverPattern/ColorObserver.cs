using UnityEngine;

public class ColorObserver : MonoBehaviour, IObserver
{
    public PanelSubject subject;

    private SpriteRenderer rend;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();

        // Subscribe to the subject
        subject.AddObserver(this);
    }

    public void OnNotify()
    {
        // Change shader color
        rend.material.SetColor("_TargetColor", Color.green);
    }
}