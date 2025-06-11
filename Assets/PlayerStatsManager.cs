using System.IO;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    public int playedGames;
    public int wonGames;
    public int lostGames;
    public float totalPlayTime; // în secunde

    public PlayerStats()
    {
        string statsFilePath = Application.persistentDataPath + "/playerStats.txt";

        if (File.Exists(statsFilePath))
        {
            try
            {
                string json = File.ReadAllText(statsFilePath);

                // Verifică dacă fișierul nu este gol
                if (!string.IsNullOrEmpty(json))
                {
                    PlayerStats loadedStats = JsonUtility.FromJson<PlayerStats>(json);

                    // Atribuie valorile din fișier
                    this.playedGames = loadedStats.playedGames;
                    this.wonGames = loadedStats.wonGames;
                    this.lostGames = loadedStats.lostGames;
                    this.totalPlayTime = loadedStats.totalPlayTime;

                    Debug.Log("Stats loaded from file successfully!");
                }
                else
                {
                    // Fișierul este gol - inițializează cu 0
                    InitializeWithZero();
                    Debug.Log("File exists but is empty - initialized with 0");
                }
            }
            catch (System.Exception e)
            {
                // Eroare la citirea/parsarea JSON - inițializează cu 0
                Debug.LogError("Error loading stats: " + e.Message);
                InitializeWithZero();
            }
        }
        else
        {
            // Fișierul nu există - inițializează cu 0
            InitializeWithZero();
            Debug.Log("Stats file not found - initialized with 0");
        }
    }

    private void InitializeWithZero()
    {
        this.playedGames = 0;
        this.wonGames = 0;
        this.lostGames = 0;
        this.totalPlayTime = 0f;
    }
}

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager instance;
    public PlayerStats stats = new PlayerStats();

    private string statsFilePath;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // păstrează între scene
            statsFilePath = Application.persistentDataPath + "/playerStats.txt";
            LoadStats();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveStats()
    {
        string json = JsonUtility.ToJson(stats);
        File.WriteAllText(statsFilePath, json);
    }

    public void LoadStats()
    {
        if (File.Exists(statsFilePath))
        {
            string json = File.ReadAllText(statsFilePath);
            stats = JsonUtility.FromJson<PlayerStats>(json);
        }
        else
        {
            stats = new PlayerStats();
        }
    }

    // apelabile din alte scripturi:
    public void AddGame(bool won)
    {
        stats.playedGames++;
        if (won) stats.wonGames++;
        else stats.lostGames++;
        SaveStats();
    }


    public void AddDraw(bool draw)
    {
        if (draw)
        {
            stats.playedGames++;
            // Nu incrementăm wonGames sau lostGames pentru egalitate
        }
        SaveStats();
    }


    public void AddPlayTime(float seconds)
    {
        stats.totalPlayTime += seconds;
        SaveStats();
    }
}
