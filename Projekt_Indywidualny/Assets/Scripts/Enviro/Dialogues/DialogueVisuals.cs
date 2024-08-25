using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueVisuals : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private void OnEnable()
    {
        DialogueInteraction.DialogueActionCallback += ShowText;

    }

    private void OnDisable()
    {
        DialogueInteraction.DialogueActionCallback -= ShowText;
    }

    public void ShowText(DialogueLine line)
    {
        StopAllCoroutines();
        StartCoroutine(ShowTextCoroutine(line));
    }

    IEnumerator ShowTextCoroutine(DialogueLine line)
    {
        dialogueBox.SetActive(true);
        dialogueText.text = line.DialogueLineText;
        yield return new WaitForSeconds(line.DialogueLineTime);
        dialogueBox.SetActive(false);
    }
}
