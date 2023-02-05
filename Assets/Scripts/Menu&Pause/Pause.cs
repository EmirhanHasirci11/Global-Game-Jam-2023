using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public Options options;
    public GameObject optionsmenu;
    public GameObject pausescreen;
    public GameObject pausemenu;
    public float musicmultiplier;
    Scene scene;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        PauseGame();
        Continue();
    }
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (Time.timeScale != 0)
            {
                PauseGame();
            }
            else
            {
                Continue();
            }

        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausescreen.SetActive(true);
        musicmultiplier = 0.2f;
        options.s.SetVolume();
        for (int i = 0; i < options.s.sounds.Length; i++)
        {
            if (options.s.sounds[i].isMusic)
            {
                options.s.sounds[i].source.Pause();
            }
        }
    }
    public void Continue()
    {
        musicmultiplier = 1;
        options.s.SetVolume();
        Time.timeScale = 1;
        pausescreen.SetActive(false);
        for (int i = 0; i < options.s.sounds.Length; i++)
        {
            if (options.s.sounds[i].isMusic)
            {
                options.s.sounds[i].source.Play();
            }
        }
    }
    public void OptionsMenu()
    {
        optionsmenu.SetActive(true);
        pausemenu.SetActive(false);
    }
    public void Close()
    {
        pausemenu.SetActive(true);
        optionsmenu.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(scene.name);
    }
}
