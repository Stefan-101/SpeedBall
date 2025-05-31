using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomCar : MonoBehaviour
{
    public Image carImage;
    public Sprite[] carSprites;

    private int index = 0;

    void Start()
    {
        index = PlayerPrefs.GetInt("SelectedCar", 0); //by default, the selected car is the first one, the default one from the game
        UpdateCarDisplay();
    }

    public void OnClickLeft()
    {
        index--;
        if (index < 0) //if there are no cars left to the left
            index = carSprites.Length - 1; //go back to the last one, like in a circle
        UpdateCarDisplay();
    }

    public void OnClickRight()
    {
        index++;
        if (index >= carSprites.Length) //if there are no cars left to the right
            index = 0; //go back to the first one, like in a cirle 
        UpdateCarDisplay();
    }

    public void OnClickSelect()
    {
        PlayerPrefs.SetInt("SelectedCar", index);
        PlayerPrefs.Save();
        Debug.Log("Selected the car with index: " + index);
    }
    void UpdateCarDisplay() //showing the selected car in UI
    {
        if (carSprites.Length > 0)
            carImage.sprite = carSprites[index];
    }
    public void GoToStartMenu() //when pressing exit
    {
        SceneManager.LoadScene("StartMenu");
    }

}
