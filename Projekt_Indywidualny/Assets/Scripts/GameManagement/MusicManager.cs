using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Header("Default Music Configuration")]
    [SerializeField] private AudioSource defaultMusicSource;

    [Header("Fight Music Configuration")]
    [SerializeField] private AudioSource fightMusicSource;


    [Header("Special Music Configuration")]
    [SerializeField] private AudioSource specialMusicSourceOne;
    [SerializeField] private AudioSource specialMusicSourceTwo;

    [Header("Fade Configuration")]
    [SerializeField] private float fadeTime = 1f;
    [SerializeField] private int fadeSteps = 10;


    private AudioSource activeAudioSource;
    private bool fading = false;
    #region Singleton
    public static MusicManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("More than one MusicManager instance found!");
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    private void Start()
    {
        PlayDefaultMusic();
    }


    public void PlayFightMusic()
    {
        if (activeAudioSource != null && !fading)
        {
            StartCoroutine(FadeMusic(fightMusicSource));
        }
    }

    public void PlayDefaultMusic()
    {
        if (activeAudioSource != null && !fading)
        {
            StartCoroutine(FadeMusic(defaultMusicSource));
        }
        else
        {
            activeAudioSource = defaultMusicSource;
            defaultMusicSource.Play();
        }
    }

    public void PlaySpecialMusic(AudioClip music)
    {
        if (specialMusicSourceOne.isPlaying)
        {
            specialMusicSourceTwo.clip= music;
            StartCoroutine(FadeMusic(specialMusicSourceTwo));
        }
        else
        {
            specialMusicSourceOne.clip = music;
            StartCoroutine(FadeMusic(specialMusicSourceOne));
        }
    }

    IEnumerator FadeMusic(AudioSource nextAudioSource)
    {
        fading = true;
        nextAudioSource.Play();
        nextAudioSource.volume = 0f;

        for (int i = 0; i< fadeSteps; i++)
        {
            activeAudioSource.volume = (fadeSteps - i) / (float)fadeSteps;

            nextAudioSource.volume = i/(float)fadeSteps;

            yield return new WaitForSeconds((float)(fadeTime /fadeSteps));
            Debug.Log((float)(fadeTime / fadeSteps));
        }
        activeAudioSource.Stop();
        activeAudioSource = nextAudioSource;
        fading = false;
    }
}
