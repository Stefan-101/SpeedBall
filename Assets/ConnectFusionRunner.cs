using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Loading Lobby...");
        SceneManager.LoadScene("Lobby 1"); // Replace with your actual lobby scene name
    }
    //private async void Start()
    //{
    //    Debug.Log("Starting Fusion runner...");

    //    NetworkRunner runner = new GameObject("NetworkRunner").AddComponent<NetworkRunner>();
    //    runner.ProvideInput = true;

    //    DontDestroyOnLoad(runner); // Persist between scenes

    //    // Get the build index of the Lobby scene

    //    int lobbyIndex = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/Lobby 1.unity");

    //    if (lobbyIndex == -1)
    //    {
    //        Debug.LogError("Scene 'Lobby 1' not found in Build Settings or incorrect path.");
    //        return;
    //    }

    //    SceneRef sceneRef = SceneRef.FromIndex(lobbyIndex);


    //    var result = await runner.StartGame(new StartGameArgs()
    //    {
    //        GameMode = GameMode.Shared,
    //        SessionName = "InitSession",
    //        Scene = sceneRef,
    //        SceneManager = runner.gameObject.AddComponent<NetworkSceneManagerDefault>()
    //    });

    //    if (result.Ok)
    //    {
    //        Debug.Log("Connected to server. Lobby scene is loading via Fusion...");
    //        // ❌ DO NOT manually load scene here
    //    }
    //    else
    //    {
    //        Debug.LogError("Failed to connect: " + result.ShutdownReason);
    //    }
    //}
}
