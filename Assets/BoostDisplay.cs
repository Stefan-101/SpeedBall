using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoostDisplay : MonoBehaviour
{
    public CarMovement carMovement; // Assign in Inspector
    public TMP_Text boostText;          // Assign in Inspector

    void Update()
    {
        if (carMovement != null && boostText != null)
        {
            boostText.text = $"{carMovement.remainingBoost:F0}";
        }
    }
}
