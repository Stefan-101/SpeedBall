//using Fusion;
//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;
//using UnityEngine.SceneManagement;

//public class LobbyUIManager : MonoBehaviour
//{
//    public TMP_InputField roomNameInput;
//    public Button createRoomButton;
//    public Button joinRoomButton;

//    private async void Start()
//    {
//        createRoomButton.onClick.AddListener(() => StartGame(GameMode.Host));
//        joinRoomButton.onClick.AddListener(() => StartGame(GameMode.Client));
//    }

//    async void StartGame(GameMode mode)
//    {
//        string roomName = roomNameInput.text.Trim();
//        if (string.IsNullOrEmpty(roomName))
//        {
//            Debug.LogWarning("Room name is empty!");
//            return;
//        }
//        var runnerGO = new GameObject("NetworkRunner");
//        var runner = runnerGO.AddComponent<NetworkRunner>();
//        runner.ProvideInput = true;

//    
//        var sceneManager = runnerGO.AddComponent<NetworkSceneManagerDefault>();

//     
//        int sceneIndex = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/1v1Scene 1.unity");
//        if (sceneIndex < 0)
//        {
//            Debug.LogError("Scene '1v1Scene' not found in Build Settings.");
//            return;
//        }

//        var startArgs = new StartGameArgs()
//        {
//            GameMode = mode,
//            SessionName = roomName,
//            Scene = SceneRef.FromIndex(sceneIndex),
//            SceneManager = sceneManager
//        };

//        var result = await runner.StartGame(startArgs);

//        if (result.Ok)
//        {
//            Debug.Log($"{mode} started for room '{roomName}'");
//        }
//        else
//        {
//            Debug.LogError($"Failed to start {mode}: {result.ShutdownReason}");
//        }
//    }
//}

//using Fusion;
//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;
//using UnityEngine.SceneManagement;

//public class LobbyUIManager : MonoBehaviour
//{
//    public TMP_InputField roomNameInput;
//    public Button createRoomButton;
//    public Button joinRoomButton;

//    private NetworkRunner runner;

//    private void Start()
//    {
//        createRoomButton.onClick.AddListener(() => StartGame(GameMode.Host));
//        joinRoomButton.onClick.AddListener(() => StartGame(GameMode.Client));
//    }

//    async void StartGame(GameMode mode)
//    {
//        string roomName = roomNameInput.text.Trim();

//        if (string.IsNullOrEmpty(roomName))
//        {
//            Debug.LogWarning("Room name is empty!");
//            return;
//        }

//        if (runner != null)
//        {
//            Debug.LogWarning("Already running a NetworkRunner.");
//            return;
//        }

//      
//        var runnerGO = new GameObject("NetworkRunner");
//        runner = runnerGO.AddComponent<NetworkRunner>();
//        runner.ProvideInput = true;

//       
//        var sceneManager = runnerGO.AddComponent<NetworkSceneManagerDefault>();

//        
//        int sceneIndex = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/1v1Scene 1.unity");
//        if (sceneIndex < 0)
//        {
//            Debug.LogError("Scene '1v1Scene 1' not found in Build Settings!");
//            return;
//        }
//        var startArgs = new StartGameArgs()
//        {
//            GameMode = mode,
//            SessionName = roomName,
//            Scene = SceneRef.FromIndex(sceneIndex),
//            SceneManager = sceneManager
//        };

//        var result = await runner.StartGame(startArgs);

//        if (result.Ok)
//        {
//            Debug.Log($"{mode} started successfully for room '{roomName}'");
//        }
//        else
//        {
//            Debug.LogError($"Failed to start {mode}: {result.ShutdownReason}");
//        }
//    }
//}

using Fusion;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LobbyUIManager : MonoBehaviour
{
    public TMP_InputField roomNameInput;
    public Button createRoomButton;
    public Button joinRoomButton;

    private NetworkRunner runner;

    private void Start()
    {
        createRoomButton.onClick.AddListener(() => StartGame(GameMode.Host));
        joinRoomButton.onClick.AddListener(() => StartGame(GameMode.Client));
    }

    async void StartGame(GameMode mode)
    {
        string roomName = roomNameInput.text.Trim();
        if (string.IsNullOrEmpty(roomName))
        {
            Debug.LogWarning("Room name is empty!");
            return;
        }

        if (runner != null)
        {
            Debug.LogWarning("Already running a NetworkRunner.");
            return;
        }

        var runnerGO = new GameObject("NetworkRunner");
        runner = runnerGO.AddComponent<NetworkRunner>();
        runner.ProvideInput = true;

        var sceneManager = runnerGO.AddComponent<NetworkSceneManagerDefault>();

        int sceneIndex = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/1v1Scene 1.unity");
        if (sceneIndex < 0)
        {
            Debug.LogError("Scene '1v1Scene 1' not found in Build Settings!");
            return;
        }

        var startArgs = new StartGameArgs()
        {
            GameMode = mode,
            SessionName = roomName,
            Scene = SceneRef.FromIndex(sceneIndex),
            SceneManager = sceneManager
        };

        var result = await runner.StartGame(startArgs);

        if (result.Ok)
        {
            Debug.Log($"{mode} started successfully for room '{roomName}'");

            Debug.Log("TickRate is set in NetworkProjectConfig asset in the Unity Editor.");
        }
        else
        {
            Debug.LogError($"Failed to start {mode}: {result.ShutdownReason}");
        }
    }
}



