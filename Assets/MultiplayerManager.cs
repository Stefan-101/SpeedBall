using UnityEngine;
using Unity.Netcode;

public class MultiplayerManager : MonoBehaviour
{
    public GameObject player1Object; // Reference to player 1 GameObject in scene
    public GameObject player2Object; // Reference to player 2 GameObject in scene

    private void Start()
    {
        // Only host can spawn players
        if (NetworkManager.Singleton.IsHost)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        }
    }

    private void OnDestroy()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
        }
    }

    private void OnClientConnected(ulong clientId)
    {
        // Wait for both players to be connected
        if (NetworkManager.Singleton.ConnectedClientsList.Count == 2)
        {
            SpawnPlayersIfNeeded();
        }
    }

    private void SpawnPlayersIfNeeded()
    {
        if (!NetworkManager.Singleton.IsHost)
            return;

        ulong hostId = NetworkManager.ServerClientId;

        // Find the client ID
        bool clientFound = false;
        ulong clientId = 0;

        foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
        {
            if (client.ClientId != hostId)
            {
                clientId = client.ClientId;
                clientFound = true;
                break;
            }
        }

        if (!clientFound)
            return;

        // Randomly assign host/client to left or right
        bool hostIsLeft = Random.value > 0.5f;

        GameObject leftPlayer = hostIsLeft ? player1Object : player2Object;
        GameObject rightPlayer = hostIsLeft ? player2Object : player1Object;

        // Assign ownership and spawn players
        if (!leftPlayer.GetComponent<NetworkObject>().IsSpawned)
        {
            leftPlayer.GetComponent<NetworkObject>().SpawnWithOwnership(hostIsLeft ? hostId : clientId);
        }

        if (!rightPlayer.GetComponent<NetworkObject>().IsSpawned)
        {
            rightPlayer.GetComponent<NetworkObject>().SpawnWithOwnership(hostIsLeft ? clientId : hostId);
        }

        // OPTIONAL: Update input config depending on side
        var leftCar = leftPlayer.GetComponent<CarMovement>();
        var rightCar = rightPlayer.GetComponent<CarMovement>();

        if (leftCar != null)
            leftCar.inputConfig.isLeftPlayer = true;

        if (rightCar != null)
            rightCar.inputConfig.isLeftPlayer = false;
    }
}
