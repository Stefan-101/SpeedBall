using Photon.Pun;
using UnityEngine;

public class ServerManager : MonoBehaviourPunCallbacks
{
    [Header("Prefabs (din Resources)")]
    public string playerPrefabName = "Player";
    public string ballPrefabName = "Ball";

    [Header("Poziții de spawn")]
    public Transform player1SpawnPoint;
    public Transform player2SpawnPoint;
    public Transform ballSpawnPoint;

    void Start()
    {
        SpawnPlayer();

        if (PhotonNetwork.IsMasterClient)
        {
            SpawnBall();
        }
    }

    public void SpawnPlayer()
    {
        Transform spawnPoint = PhotonNetwork.IsMasterClient ? player1SpawnPoint : player2SpawnPoint;

        if (spawnPoint == null)
        {
            Debug.LogError("Spawn point-ul pentru player nu e setat în GameManager!");
            return;
        }

        PhotonNetwork.Instantiate(playerPrefabName, spawnPoint.position, spawnPoint.rotation);
    }

    public void SpawnBall()
    {
        if (ballSpawnPoint == null)
        {
            Debug.LogError("Spawn point-ul pentru minge nu e setat în GameManager!");
            return;
        }
        //PhotonNetwork.Instantiate("Ball", Vector2.zero, Quaternion.identity);
        PhotonNetwork.Instantiate(ballPrefabName, ballSpawnPoint.position, ballSpawnPoint.rotation);
    }

    public override void OnLeftRoom()
    {
        // Opțional: revino în Lobby când pleci din cameră
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
    }
}
