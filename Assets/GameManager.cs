using System.Collections;
using System.Diagnostics.CodeAnalysis;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int scoreLeft = 0;
    public int scoreRight = 0;

    public GameObject leftPlayer;
    public GameObject rightPlayer;
    public TMP_Text score;
    public TMP_Text startTime;
    bool isFreeze = true;
    public float freezeTime = 3f;

    public Sprite[] digitSprites;
    public Image[] leftScoreImages;      
    public Image[] rightScoreImages;


    //Timer-ul

    public Image[] imageSlots;        // Sloturi UI → 4 imagini pentru MMSS
                                         // Sprite-uri de scor: 0–9
    public float elapsedTime = 120f;

    void Start()
    {
        FreezeAndStartAfterDelay(freezeTime);
        SetScoreImages(0, leftScoreImages);
        SetScoreImages(0, rightScoreImages);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime -= Time.deltaTime;

        int totalSeconds = Mathf.FloorToInt(elapsedTime);
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        string timeStr = minutes.ToString("00") + seconds.ToString("00");

        SetTimpImages(timeStr, imageSlots);

        if (elapsedTime <= 0.0f)
        {
            timerEnded();
        }



    }
    void timerEnded()
    {
        // Game Over logic
        Debug.Log("Game Over!");
        Time.timeScale = 0; // Stop the game
        startTime.text = "Game Over!";


        if(scoreLeft > scoreRight)
        {
            startTime.text += "\nLeft Player Wins!";

            PlayerStatsManager.instance.AddGame(true); // dacă a câștigat
            PlayerStatsManager.instance.AddPlayTime(Time.deltaTime); // în Update()

        }
        else if (scoreRight > scoreLeft)
        {
            startTime.text += "\nRight Player Wins!";
        }
        else
        {
            startTime.text += "\nIt's a Draw!";

            PlayerStatsManager.instance.AddDraw(true); // dacă a pierdut
        }
        FreezeAndStartAfterDelay(2f); // Optionally, freeze the game for a few seconds before resetting

    }

    void SetScoreImages(int score, Image[] imageSlots)
    {
        //always 2 digits (0x or xx)
        string scoreStr = score.ToString().PadLeft(2, '0');

        //reset
        foreach (var img in imageSlots)
            img.enabled = false;

        int offset = imageSlots.Length - scoreStr.Length;

        for (int i = 0; i < scoreStr.Length; i++)
        {
            int digit = scoreStr[i] - '0';
            imageSlots[offset + i].sprite = digitSprites[digit];
            imageSlots[offset + i].enabled = true;
        }
    }

    void SetTimpImages(string timeStr, Image[] imageSlots)
    {
        for (int i = 0; i < imageSlots.Length && i < timeStr.Length; i++)
        {
            int digit = timeStr[i] - '0';
            imageSlots[i].sprite = digitSprites[digit];
            imageSlots[i].enabled = true;
        }
    }



    void UpdateScoreImages()
    {
        SetScoreImages(scoreLeft, leftScoreImages);
        SetScoreImages(scoreRight, rightScoreImages);
    }
    public void ResetGame()
    {
        // Clear all boost pickups from the map
        foreach (var boost in FindObjectsOfType<BoostPickup>())
        {
            if (boost.tag != null && boost.tag == "CloneBoostOrb")
            {
                Destroy(boost.gameObject);
            }
        }

        leftPlayer.transform.position = new Vector3(-20, 0, 0);
        leftPlayer.transform.rotation = Quaternion.identity; // Reset rotation
        leftPlayer.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        leftPlayer.GetComponent<Rigidbody2D>().angularVelocity = 0;
        leftPlayer.GetComponent<CarMovement>().ResetBoost();

        rightPlayer.transform.position = new Vector3(20, 0, 0);
        rightPlayer.transform.rotation = Quaternion.identity; // Reset rotation
        rightPlayer.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        rightPlayer.GetComponent<Rigidbody2D>().angularVelocity = 0;
        rightPlayer.GetComponent<CarMovement>().ResetBoost();

        UpdateScoreImages();
        FreezeAndStartAfterDelay(freezeTime);
    }
    void FreezeAndStartAfterDelay(float duration)
    {
        Time.timeScale = 0;
        StartCoroutine(CountdownAndStartGame(duration));
    }

    IEnumerator CountdownAndStartGame(float duration)
    {
        float remainingTime = duration;

        while (remainingTime > 0)
        {
            startTime.text = "Game starts in: " + Mathf.CeilToInt(remainingTime);
            yield return new WaitForSecondsRealtime(1f);
            remainingTime -= 1f;
        }

        startTime.text = "";
        Time.timeScale = 1;
    }
}
