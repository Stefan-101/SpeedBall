using UnityEngine;
using UnityEngine.UI;

public class CarLoader : MonoBehaviour
{
    public Sprite[] carSprites; //car sprites from CustomCarMenu
    public SpriteRenderer car; //car for Player2

    void Start()
    {
        int selectedIndex = PlayerPrefs.GetInt("SelectedCar", 0);
        if (selectedIndex >= 0 && selectedIndex < carSprites.Length)
            car.sprite = carSprites[selectedIndex]; //the current car is the selected one from the CustomCarMenu (default car if not choosen)
        else
            Debug.LogWarning("Wrong car index!");
    }
}
