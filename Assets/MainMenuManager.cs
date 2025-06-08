using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;


public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenuContent;    
    public GameObject customCarMenuPanel;
    public GameObject userStatsPanel;
    public GameObject playContent;
    public void PlayButton()
    {
        AudioManager.instance.PlayClickSound();
        mainMenuContent.SetActive(false);
        playContent.SetActive(true);
    }
    public void Play()
    {
        AudioManager.instance.PlayClickSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1 );
    }
    public void Multiplayer()
    {
        AudioManager.instance.PlayClickSound();
        SceneManager.LoadScene("Lobby 1");
    }
    public void ShowCustomCarMenu()
    {
        AudioManager.instance.PlayClickSound();
        mainMenuContent.SetActive(false);  //hide main menu
        customCarMenuPanel.SetActive(true); //show custom car menu
    }
 
    public void ShowUserStats()
    {
        AudioManager.instance.PlayClickSound();
        mainMenuContent.SetActive(false);
        userStatsPanel.SetActive(true);
    }
    public void Quit()
    {
        AudioManager.instance.PlayClickSound();
        Application.Quit();
        Debug.Log("Quit");
    }
}