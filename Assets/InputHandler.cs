//using Fusion;
//using Fusion.Sockets;
//using UnityEngine;

//public class InputHandler : MonoBehaviour, INetworkRunnerCallbacks
//{
//    private NetworkRunner runner;

//    private void Start()
//    {
//        runner = FindObjectOfType<NetworkRunner>();
//        if (runner == null)
//        {
//            Debug.LogError("NetworkRunner not found");
//        }
//        else
//        {
//            runner.AddCallbacks(this);
//        }
//    }

//    public void OnInput(NetworkRunner runner, NetworkInput input)
//    {
//        NetworkInputData data = new NetworkInputData
//        {
//            move = new Vector2(Input.GetAxis("Horizontal"), 0),
//            jump = Input.GetButtonDown("Jump"),
//            boost = Input.GetButton("Boost")
//        };

//        input.Set(data);
//    }

//    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) { }
//    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
//    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
//    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
//    public void OnConnectedToServer(NetworkRunner runner) { }
//    public void OnDisconnectedFromServer(NetworkRunner runner) { }
//    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
//    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
//    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
//    public void OnSessionListUpdated(NetworkRunner runner, System.Collections.Generic.List<SessionInfo> sessionList) { }
//    public void OnCustomAuthenticationResponse(NetworkRunner runner, System.Collections.Generic.Dictionary<string, object> data) { }
//    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
//    public void OnSceneLoadDone(NetworkRunner runner) { }
//    public void OnSceneLoadStart(NetworkRunner runner) { }
//    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
//    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
//    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
//    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, System.ArraySegment<byte> data) { }
//    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
//}
