using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private GameObject insertDiskScreen;
    [SerializeField] private TextMeshProUGUI insertDiskText;
    [SerializeField] private AudioSource menuAudioSource;
    [SerializeField] private AudioClip loadDiskSound;
    [SerializeField] private AudioClip mainMenuMusic;
    private bool isMenuLoaded = false;
    public void StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene("Nexus");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    private void Update()
    {
        if (Input.anyKey && !isMenuLoaded)
        {
            isMenuLoaded= true;
            StartCoroutine("LoadMenu");
        }

    }

    private IEnumerator LoadMenu()
    {
        insertDiskText.text = ">Reading Disk<";
        menuAudioSource.clip= loadDiskSound;
        menuAudioSource.Play();
        yield return new WaitForSeconds(menuAudioSource.clip.length);
        mainMenuScreen.SetActive(true);
        insertDiskScreen.SetActive(false);
        menuAudioSource.clip = mainMenuMusic;
        menuAudioSource.loop=true;
        menuAudioSource.Play();
    }

}
