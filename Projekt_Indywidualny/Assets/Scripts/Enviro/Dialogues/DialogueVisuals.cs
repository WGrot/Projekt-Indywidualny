using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueVisuals : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Image dialogueImage;

    private void OnEnable()
    {
        DialogueInteraction.DialogueActionCallback += ShowVisuals;

    }

    private void OnDisable()
    {
        DialogueInteraction.DialogueActionCallback -= ShowVisuals;
    }

    public void ShowVisuals(DialogueLine line)
    {
        StopAllCoroutines();
        StartCoroutine(ShowTextCoroutine(line));
    }

    IEnumerator ShowTextCoroutine(DialogueLine line)
    {
        dialogueBox.SetActive(true);
        dialogueText.text = line.DialogueLineText;
        if(line.DialogueImage != null)
        {
           dialogueImage.sprite = line.DialogueImage;
        }
        yield return new WaitForSeconds(line.DialogueLineTime);
        dialogueBox.SetActive(false);
    }
}
