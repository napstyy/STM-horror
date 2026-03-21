using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject nameBox;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject continueArrow;

    [Header("Settings")]
    [SerializeField] private float charsPerSecond = 40f;

    // State
    private DialogueData currentData;
    private int currentLineIndex;
    private bool isTyping;
    private bool waitingForInput;
    private Coroutine typewriterCoroutine;

    public bool IsOpen => dialoguePanel.activeSelf;

    public event System.Action OnDialogueStart;
    public event System.Action OnDialogueEnd;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (!IsOpen) return;

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (isTyping)
                SkipTypewriter();
            else if (waitingForInput)
                Advance();
        }
    }

    public void StartDialogue(DialogueData data)
    {
        currentData = data;
        currentLineIndex = 0;

        bool hasSpeaker = !string.IsNullOrWhiteSpace(data.speakerName);
        nameBox.SetActive(hasSpeaker);
        if (hasSpeaker) nameText.text = data.speakerName;

        dialoguePanel.SetActive(true);
        OnDialogueStart?.Invoke();
        ShowCurrentLine();
    }

    private void ShowCurrentLine()
    {
        continueArrow.SetActive(false);
        waitingForInput = false;

        if (typewriterCoroutine != null) StopCoroutine(typewriterCoroutine);
        typewriterCoroutine = StartCoroutine(TypeLine(currentData.lines[currentLineIndex]));
    }

    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        float delay = 1f / charsPerSecond;
        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(delay);
        }

        isTyping = false;
        continueArrow.SetActive(true);
        waitingForInput = true;
    }

    private void SkipTypewriter()
    {
        if (typewriterCoroutine != null) StopCoroutine(typewriterCoroutine);
        isTyping = false;
        dialogueText.text = currentData.lines[currentLineIndex];
        continueArrow.SetActive(true);
        waitingForInput = true;
    }

    private void Advance()
    {
        currentLineIndex++;

        if (currentLineIndex >= currentData.lines.Length)
            EndDialogue();
        else
            ShowCurrentLine();
    }

    private void EndDialogue()
    {
        if (typewriterCoroutine != null) StopCoroutine(typewriterCoroutine);
        dialoguePanel.SetActive(false);
        currentData = null;
        OnDialogueEnd?.Invoke();
    }
}