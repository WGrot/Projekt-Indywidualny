using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DialogueInteraction : MonoBehaviour, I_Interactable
{
    [SerializeField] private bool playSound = true;
    [SerializeField] private List<DialogueLine> dialogueLines;
    [SerializeField] private AudioSource audioSource;
    InputActions inputActions;

    public UnityEvent OnDialogueEnded;
    private bool wasEndEventTriggered = false;

    private bool isDialogueActive = false;
    private int activeLineID = -1;
    public void InteractWithPlayer()
    {
        StartDialogue();
    }

    private void Awake()
    {
        inputActions = new InputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player_base.Interact.performed += _ => ManageDialogue();
        GameStateManager.GameStateChangeCallback += PauseRunAudio;
        
    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.Player_base.Interact.performed -= _ => ManageDialogue();
        GameStateManager.GameStateChangeCallback -= PauseRunAudio;
    }

    void PauseRunAudio(PauseState state)
    {
        if(state == PauseState.Running && isDialogueActive)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }

    public delegate void DialogueAction(DialogueLine line);
    public static event DialogueAction DialogueActionCallback;

    public void ManageDialogue()
    {

        if (isDialogueActive)
        {
            SkipToNextLine();
        }

    }

    public void StartDialogue()
    {

        if (!isDialogueActive)
        {

            activeLineID = 0;
            StartCoroutine(PlayDialogue());
        }
    }

    public IEnumerator PlayDialogue()
    {
        yield return null;
        isDialogueActive = true;
        for (int i = activeLineID; i < dialogueLines.Count; i++)
        {

            activeLineID++;
            DialogueLine line = dialogueLines[i];
            if (DialogueActionCallback != null)
            {
                DialogueActionCallback(line);
            }

            if (playSound)
            {
                audioSource.clip = line.DialogueLineAudio;
                audioSource.Play();
            }
            yield return new WaitForSeconds(line.DialogueLineTime);

        }
        activeLineID = 0;
        isDialogueActive = false;
        if (!wasEndEventTriggered)
        {
            wasEndEventTriggered = true;
            OnDialogueEnded.Invoke();
        }
    }

    public void SkipToNextLine()
    {
        StopAllCoroutines();
        StartCoroutine(PlayDialogue());
    }



}
