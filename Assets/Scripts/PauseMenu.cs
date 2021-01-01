using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool gameIsPaused;
    private GameObject startMenuUI;
    public AudioSource pauseSound;
    public AudioSource resumeSound;

    private void Start()
    {
        startMenuUI = GameObject.FindGameObjectsWithTag("PauseMenu")[0];
        startMenuUI.SetActive(false);
        gameIsPaused = false;

        Time.timeScale = 1f;

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameIsPaused)
        {
            Pause();   
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gameIsPaused)
        {
            Resume();
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("StartMenu");

    }

    public void Pause()
    {
        gameIsPaused = true;
        startMenuUI.SetActive(true);
        Time.timeScale = 0f;
        pauseSound.Play();
    }

    public void Resume()
    {
        gameIsPaused = false;
        startMenuUI.SetActive(false);
        Time.timeScale = 1f;
        resumeSound.Play();
    }
}
