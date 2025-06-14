using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] public GameObject pauseMenu;
    public void LoadStartMenu()
    {
        AudioManager.instance.PlayClickSound();
        SceneManager.LoadScene("StartMenu");
        Time.timeScale = 1;
    }
    public void Pause()
    {
        AudioManager.instance.PlayClickSound();
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        AudioManager.instance.PlayClickSound();
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void Restart()
    {
        AudioManager.instance.PlayClickSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
