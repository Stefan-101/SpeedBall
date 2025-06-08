using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField createRoomInput;
    public TMP_InputField joinRoomInput;
    public Button createRoomButton;
    public Button joinRoomButton;

    private void Start()
    {
        createRoomButton.onClick.AddListener(CreateRoom);
        joinRoomButton.onClick.AddListener(JoinRoom);

        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectat la Master Server");
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void CreateRoom()
    {
        string roomName = createRoomInput.text.Trim();
        if (string.IsNullOrEmpty(roomName))
        {
            Debug.LogWarning("Numele camerei nu poate fi gol!");
            return;
        }

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;

        PhotonNetwork.CreateRoom(roomName, options);
    }

    private void JoinRoom()
    {
        string roomName = joinRoomInput.text.Trim();
        if (string.IsNullOrEmpty(roomName))
        {
            Debug.LogWarning("Numele camerei nu poate fi gol!");
            return;
        }

        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Camera creată cu succes!");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Te-ai conectat la camera: " + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel("1v1Scene");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Eroare creare cameră: " + message);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Eroare conectare cameră: " + message);
    }
}
