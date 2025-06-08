//using Fusion;
//using Fusion.Sockets;
//using System.Collections.Generic;
//using System;
//using UnityEngine;

//public class NetworkCallbacks : MonoBehaviour, INetworkRunnerCallbacks
//{
//    [SerializeField] private GameManagerMultiplayer gameManager;

//    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
//    {
//        if (runner.IsServer)
//        {
//            gameManager.SpawnPlayer(runner, player);
//        }
//    }

//    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
//    public void OnInput(NetworkRunner runner, NetworkInput input) { }
//    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
//    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
//    public void OnConnectedToServer(NetworkRunner runner) { }
//    public void OnDisconnectedFromServer(NetworkRunner runner) { }
//    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
//    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
//    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
//    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
//    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
//    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
//    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }
//    public void OnSceneLoadDone(NetworkRunner runner) { }
//    public void OnSceneLoadStart(NetworkRunner runner) { }

//    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
//    {
//        throw new NotImplementedException();
//    }

//    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
//    {
//        throw new NotImplementedException();
//    }

//    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
//    {
//        throw new NotImplementedException();
//    }

//    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
//    {
//        throw new NotImplementedException();
//    }

//    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
//    {
//        throw new NotImplementedException();
//    }
//}
