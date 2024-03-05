using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private PauseState pauseState = PauseState.Running;
    public PauseState GetPauseState()
    {
        return pauseState;
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
    }
}
