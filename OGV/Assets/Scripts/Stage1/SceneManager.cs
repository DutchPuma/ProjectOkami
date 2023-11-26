using UnityEngine;
using Photon.Pun;

public class SceneController : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject gameCanvas;
    private bool hasJoinedRoom = false;

    private void Start()
    {
        ConnectToPhoton();
    }

    private void ConnectToPhoton()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected to PhotonNetwork!");
        JoinOrCreateRoom();
    }

    private void JoinOrCreateRoom()
    {
        PhotonNetwork.JoinOrCreateRoom("YourRoomName", new Photon.Realtime.RoomOptions { MaxPlayers = 4 }, null);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Joined Room!");
        hasJoinedRoom = true;
        ActivateGameCanvas();
    }

    private void ActivateGameCanvas()
    {
        if (gameCanvas != null)
        {
            gameCanvas.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Game canvas is not assigned!");
        }
    }

    private void Update()
    {
        if (hasJoinedRoom && playerPrefab != null)
        {
            SpawnPlayer();
            hasJoinedRoom = false; // Reset flag to prevent continuous spawning
        }
    }

    private void SpawnPlayer()
    {
        Vector3 spawnPosition = new Vector3(0f, 2f, 0f); // Example spawn position
        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition, Quaternion.identity, 0);
        }
        else
        {
            Debug.LogWarning("Photon is not connected or ready to instantiate!");
        }
    }
}
