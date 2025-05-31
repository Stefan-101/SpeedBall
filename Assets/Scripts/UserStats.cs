using UnityEngine;

public class UserStats : MonoBehaviour
{
    public GameObject userStatsPanel;
    public GameObject mainMenuPanel;

    public void ExitStats()
    {
        userStatsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
