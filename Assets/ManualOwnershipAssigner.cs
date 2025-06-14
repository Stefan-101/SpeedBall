using Unity.Netcode;
using UnityEngine;

public class ManualOwnershipAssigner : NetworkBehaviour
{
    public NetworkObject player1;
    public NetworkObject player2;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            // Host controls player1
            player1.SpawnWithOwnership(NetworkManager.Singleton.LocalClientId);

            // Wait for client to connect, then assign player2 to the client
            NetworkManager.Singleton.OnClientConnectedCallback += clientId =>
            {
                if (clientId != NetworkManager.Singleton.LocalClientId) // ignore host
                {
                    player2.SpawnWithOwnership(clientId);
                }
            };
        }
    }
}
