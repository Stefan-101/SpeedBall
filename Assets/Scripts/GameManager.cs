using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // Referințe la sistemul de scor și câmpul de joc
    public GoalSystem goalSystem;
    public SoccerFieldSetup fieldSetup;

    // Parametri de joc
    public int scoreToWin = 5;
    public float matchDuration = 180f; // 3 minute

    // UI pentru scor și timp
    public Text timerText;
    public GameObject gameOverPanel;
    public Text winnerText;

    // Stare joc
    private bool isGameActive = false;
    private float currentMatchTime;

    void Start()
    {
        // Găsim referințele necesare dacă nu sunt setate
        if (goalSystem == null)
            goalSystem = FindObjectOfType<GoalSystem>();

        if (fieldSetup == null)
            fieldSetup = FindObjectOfType<SoccerFieldSetup>();

        // Ascundem panoul de Game Over la început
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        // Începem meciul
        StartMatch();
    }

    void Update()
    {
        if (isGameActive)
        {
            // Actualizăm timpul
            currentMatchTime -= Time.deltaTime;

            // Actualizăm UI-ul pentru timer
            UpdateTimerDisplay();

            // Verificăm condiții de finalizare a meciului
            if (currentMatchTime <= 0)
            {
                EndMatch("Timp expirat");
            }
            else if (goalSystem != null &&
                    (goalSystem.leftScore >= scoreToWin || goalSystem.rightScore >= scoreToWin))
            {
                EndMatch("Scor maxim atins");
            }
        }
    }

    void StartMatch()
    {
        // Resetăm starea jocului
        currentMatchTime = matchDuration;
        isGameActive = true;

        // Resetăm scorul
        if (goalSystem != null)
        {
            goalSystem.leftScore = 0;
            goalSystem.rightScore = 0;
            goalSystem.UpdateScoreDisplay();
        }

        // Resetăm poziția mingii
        if (goalSystem != null && goalSystem.ball != null)
        {
            goalSystem.ResetBall();
        }
    }

    void EndMatch(string reason)
    {
        isGameActive = false;

        // Determinăm câștigătorul
        string winner = "Egalitate";
        if (goalSystem != null)
        {
            if (goalSystem.leftScore > goalSystem.rightScore)
                winner = "Jucătorul din stânga a câștigat!";
            else if (goalSystem.rightScore > goalSystem.leftScore)
                winner = "Jucătorul din dreapta a câștigat!";
        }

        // Afișăm panoul de Game Over
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);

            if (winnerText != null)
                winnerText.text = winner;
        }

        Debug.Log("Meci încheiat: " + reason + ". " + winner);
    }

    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentMatchTime / 60);
            int seconds = Mathf.FloorToInt(currentMatchTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    // Metodă pentru resetarea jocului (poate fi apelată de un buton UI)
    public void RestartMatch()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        StartMatch();
    }
}