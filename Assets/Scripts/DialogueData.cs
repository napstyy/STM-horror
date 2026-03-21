using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    [Tooltip("Shown in the name box above the text.")]
    public string speakerName;

    [TextArea(2, 6)]
    public string[] lines;
}