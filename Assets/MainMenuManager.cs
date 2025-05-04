using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;


public class MainMenuManager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1 );
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}