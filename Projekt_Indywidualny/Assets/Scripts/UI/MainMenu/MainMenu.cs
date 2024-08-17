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
    private bool isMenuLoading = false;
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
        if (Input.anyKeyDown && !isMenuLoading)
        {

            StartCoroutine("LoadMenuFirstTime");
        }

        if (Input.anyKeyDown && isMenuLoading)
        {
            StopAllCoroutines();
            SkipLoading();
        }

    }

    private IEnumerator LoadMenuFirstTime()
    {
        yield return null;
        isMenuLoading = true;
        insertDiskText.text = ">Reading Disk<";
        PlayerPrefs.SetFloat("M_Sensitivity", 100);
        PlayerPrefs.Save();
        menuAudioSource.clip= loadDiskSound;
        menuAudioSource.Play();
        yield return new WaitForSeconds(menuAudioSource.clip.length);
        mainMenuScreen.SetActive(true);
        insertDiskScreen.SetActive(false);
        menuAudioSource.clip = mainMenuMusic;
        menuAudioSource.loop=true;
        menuAudioSource.Play();
    }

    private void SkipLoading()
    {
        Debug.Log("skipped");
        PlayerPrefs.SetFloat("M_Sensitivity", 100);
        PlayerPrefs.Save();
        mainMenuScreen.SetActive(true);
        insertDiskScreen.SetActive(false);
        menuAudioSource.clip = mainMenuMusic;
        menuAudioSource.loop = true;
        menuAudioSource.Play();
    }

}
