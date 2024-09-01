using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum PauseState
{
    Paused,
    Running
}


public class GameStateManager : MonoBehaviour
{

    #region Singleton
    public static GameStateManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("More than one GameStateManager instance found!");
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    [SerializeField] AudioMixer mainAudioMixer;
    [SerializeField] Sprite slowedTimeBuffIcon;
    public delegate void GameStateChangeAction(PauseState state);
    public static event GameStateChangeAction GameStateChangeCallback;

    private PauseState pauseState = PauseState.Running;
    public PauseState GetPauseState()
    {
        return pauseState;
    }

    public bool IsGamePaused()
    {
        if(pauseState == PauseState.Paused)
        {
            return true;
        }
        return false;
    }

    public void PauseGame()
    {
        if (pauseState == PauseState.Paused)
        {
            Debug.LogError("Tried to pause game when it wasn't running");
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        pauseState = PauseState.Paused;
        if (GameStateChangeCallback != null)
        {
            GameStateChangeCallback(pauseState);
        }
    }

    public void ResumeGame()
    {
        if (pauseState == PauseState.Running)
        {
            Debug.LogError("Tried to resume game when it wasn't paused");
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        pauseState = PauseState.Running;
        if (GameStateChangeCallback != null)
        {
            GameStateChangeCallback(pauseState);
        }
    }


    public void StartSlowTime(float slowRate, float time)
    {
        StartCoroutine(SlowTime(slowRate, time));
    }

    public IEnumerator SlowTime(float slowRate, float time)
    {
        //Dodajemy pustego buffa aby by³o widaæ ¿e czas jest spowolniony
        Buff timeStopBuff = new Buff(slowRate * time, this, Time.time, slowedTimeBuffIcon);
        BuffManager.Instance.AddBuff(timeStopBuff);

        //Spowolniamy czas
        Time.timeScale = slowRate;
        mainAudioMixer.SetFloat("MasterPitch", slowRate);
        yield return new WaitForSeconds(slowRate*time);
        Time.timeScale = 1f;
        mainAudioMixer.SetFloat("MasterPitch", 1);
    }


    
}
