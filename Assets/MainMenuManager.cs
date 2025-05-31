using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;


public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenuContent;    
    public GameObject customCarMenuPanel;
    public GameObject userStatsPanel;
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1 );
    }
    public void ShowCustomCarMenu()
    {
        customCarMenuPanel.SetActive(true); //show custom car menu
        mainMenuContent.SetActive(false);  //hide main menu
    }
 
    public void ShowUserStats()
    {
        mainMenuContent.SetActive(false);
        userStatsPanel.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}