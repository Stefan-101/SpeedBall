using System.Collections;
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

    void Start()
    {
        FreezeAndStartAfterDelay(freezeTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

 
    public void ResetGame()
    {
        leftPlayer.transform.position = new Vector3(-20, 0, 0);
        leftPlayer.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        leftPlayer.GetComponent<Rigidbody2D>().angularVelocity = 0;
        rightPlayer.transform.position = new Vector3(20, 0, 0);
        rightPlayer.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        rightPlayer.GetComponent<Rigidbody2D>().angularVelocity = 0;

        score.text = scoreLeft + " - " + scoreRight;
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
