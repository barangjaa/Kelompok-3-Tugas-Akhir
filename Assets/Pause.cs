using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    bool gamePaused = false;

    [SerializeField] GameObject pauseMenu;
    [SerializeField] AudioSource musicSource; // <- Tambahan

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gamePaused == false)
        {
            Time.timeScale = 0;
            gamePaused = true;
            pauseMenu.SetActive(true);
            if (musicSource != null)
                musicSource.Pause(); // <- Pause music
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gamePaused == true)
        {
            Time.timeScale = 1;
            gamePaused = false;
            pauseMenu.SetActive(false);
            if (musicSource != null)
                musicSource.UnPause(); // <- Lanjutkan music
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        if (musicSource != null)
            musicSource.Pause(); // <- Pause music
    }

    public void Home()
    {
        SceneManager.LoadScene(1);
    }

    public void Continue()
    {
        Time.timeScale = 1;
        gamePaused = false;
        pauseMenu.SetActive(false);
        if (musicSource != null)
            musicSource.UnPause(); // <- Lanjutkan music
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
