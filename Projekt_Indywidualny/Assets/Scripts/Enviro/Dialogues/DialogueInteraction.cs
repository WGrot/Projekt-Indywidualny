using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteraction : MonoBehaviour, I_Interactable
{
    [SerializeField] private bool playSound = true;
    [SerializeField] private List<DialogueLine> dialogueLines;
    [SerializeField] private AudioSource audioSource;

    private bool isDialogueActive = false;
    private int activeLineID = -1;
    public void InteractWithPlayer()
    {
        ManageDialogue();
    }

    private void OnEnable()
    {
        GameStateManager.GameStateChangeCallback += PauseRunAudio;
    }

    private void OnDisable()
    {
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

        if (isDialogueActive == false)
        {
            activeLineID = 0;
            isDialogueActive = true;
            StartCoroutine(PlayDialogue());
        }
    }

    public IEnumerator PlayDialogue()
    {
        for (int i = activeLineID; i < dialogueLines.Count; i++)
        {
            activeLineID++;
            DialogueLine line = dialogueLines[i];
            Debug.Log(line.DialogueLineText);
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
    }

    public void SkipToNextLine()
    {
        StopAllCoroutines();
        StartCoroutine(PlayDialogue());
    }



}
