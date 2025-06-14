using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    //public int playedGames = 5;
    //public int wonGames = 3;
    //public int lostGames = 2;
    //public string totalPlayTime = "15:00";

    // may the following piece of code rest in peace
    //public PlayerStats()
    //{
    //    string statsFilePath = Application.persistentDataPath + "/playerStats.txt";

    //    if (File.Exists(statsFilePath))
    //    {
    //        try
    //        {
    //            string json = File.ReadAllText(statsFilePath);

    //            // Verifică dacă fișierul nu este gol
    //            if (!string.IsNullOrEmpty(json))
    //            {
    //                PlayerStats loadedStats = JsonUtility.FromJson<PlayerStats>(json);

    //                // Atribuie valorile din fișier
    //                this.playedGames = loadedStats.playedGames;
    //                this.wonGames = loadedStats.wonGames;
    //                this.lostGames = loadedStats.lostGames;
    //                this.totalPlayTime = loadedStats.totalPlayTime;

    //                Debug.Log("Stats loaded from file successfully!");
    //            }
    //            else
    //            {
    //                // Fișierul este gol - inițializează cu 0
    //                InitializeWithZero();
    //                Debug.Log("File exists but is empty - initialized with 0");
    //            }
    //        }
    //        catch (System.Exception e)
    //        {
    //            // Eroare la citirea/parsarea JSON - inițializează cu 0
    //            Debug.LogError("Error loading stats: " + e.Message);
    //            InitializeWithZero();
    //        }
    //    }
    //    else
    //    {
    //        // Fișierul nu există - inițializează cu 0
    //        InitializeWithZero();
    //        Debug.Log("Stats file not found - initialized with 0");
    //    }
    //}

}

public class StatsManager : MonoBehaviour
{
    public TMP_Text playedGames;
    public TMP_Text wonGames;
    public TMP_Text lostGames;
    public TMP_Text totalPlayTime;

    public static StatsManager instance;
    public PlayerStats stats = new PlayerStats();

    // demo purposes only
    private const string CountdownKey = "StatsCountdownTargetTime";
    private const float CountdownDuration = 5f;

    private void Start()
    {
        float targetTime;
        if (PlayerPrefs.HasKey(CountdownKey))
        {
            targetTime = PlayerPrefs.GetFloat(CountdownKey);
        }
        else
        {
            targetTime = Time.realtimeSinceStartup + CountdownDuration;
            PlayerPrefs.SetFloat(CountdownKey, targetTime);
            PlayerPrefs.Save();
        }

        float timeLeft = targetTime - Time.realtimeSinceStartup;
        if (timeLeft <= 0)
        {
            UpdateTexts();
        }
    }

    private IEnumerator UpdateTextsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        UpdateTexts();
    }

    private void UpdateTexts()
    {
        playedGames.text = "Number of games: 6";
        wonGames.text = "Left wins: 3";
        lostGames.text = "Right wins: 3";
        totalPlayTime.text = "Play time: 18:00";
    }



    //void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject); // păstrează între scene
    //        statsFilePath = Application.persistentDataPath + "/playerStats.txt";
    //        LoadStats();
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //public void SaveStats()
    //{
    //    string json = JsonUtility.ToJson(stats);
    //    File.WriteAllText(statsFilePath, json);
    //}

    //public void LoadStats()
    //{
    //    if (File.Exists(statsFilePath))
    //    {
    //        string json = File.ReadAllText(statsFilePath);
    //        stats = JsonUtility.FromJson<PlayerStats>(json);
    //    }
    //    else
    //    {
    //        stats = new PlayerStats();
    //    }
    //}

    //// apelabile din alte scripturi:
    //public void AddGame(bool won)
    //{
    //    stats.playedGames++;
    //    if (won) stats.wonGames++;
    //    else stats.lostGames++;
    //    SaveStats();
    //}


    //public void AddDraw(bool draw)
    //{
    //    if (draw)
    //    {
    //        stats.playedGames++;
    //        // Nu incrementăm wonGames sau lostGames pentru egalitate
    //    }
    //    SaveStats();
    //}


    //public void AddPlayTime(float seconds)
    //{
    //    stats.totalPlayTime += seconds;
    //    SaveStats();
    //}
}
