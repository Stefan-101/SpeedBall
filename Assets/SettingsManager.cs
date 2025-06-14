using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class SettingsManager : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public GameObject mainMenuContent;
    public GameObject settingsContent;

    Resolution[] Resolutions;
    bool isFullscreen;
    int SelectedResolutionIndex;
    List<Resolution> resolutionStringSelectedList = new List<Resolution>();

    void Start()
    {
        AudioManager.instance.PlayClickSound();
        isFullscreen = true;
        Resolutions = Screen.resolutions;
        string newRes;

        List<string> optionsResolutions = new List<string>();
        foreach (Resolution res in Resolutions)
        {
            newRes = res.width + " x " + res.height;
            if (!optionsResolutions.Contains(newRes))
            {
                optionsResolutions.Add(newRes);
                resolutionStringSelectedList.Add(res);
            }
            resolutionDropdown.AddOptions(optionsResolutions);
        }
    }

    public void ChangeResolution()
    {
        SelectedResolutionIndex = resolutionDropdown.value;
        Screen.SetResolution(resolutionStringSelectedList[SelectedResolutionIndex].width, resolutionStringSelectedList[SelectedResolutionIndex].height, isFullscreen);

    }

    public void ChangeFullscreen()
    {
        isFullscreen = !isFullscreen;
        Screen.fullScreen = isFullscreen;
        Screen.SetResolution(resolutionStringSelectedList[SelectedResolutionIndex].width, resolutionStringSelectedList[SelectedResolutionIndex].height, isFullscreen);

    }

    public void Exit()
    {
        AudioManager.instance.PlayClickSound();
        settingsContent.SetActive(false);
        mainMenuContent.SetActive(true);

    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
