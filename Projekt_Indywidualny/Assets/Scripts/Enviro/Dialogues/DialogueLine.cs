using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    [SerializeField] private string dialogueLineText;
    [SerializeField] private AudioClip dialogueLineAudio;
    [SerializeField] private float dialogueLineTime;

    public string DialogueLineText { get => dialogueLineText; private set => dialogueLineText = value; }
    public AudioClip DialogueLineAudio { get => dialogueLineAudio; private set => dialogueLineAudio = value; }
    public float DialogueLineTime { get => dialogueLineTime; set => dialogueLineTime = value; }
}
