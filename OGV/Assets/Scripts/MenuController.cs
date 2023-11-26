using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MenuController : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject UsernameMenu;
    [SerializeField] private InputField UsernameInput;
    [SerializeField] private InputField CreateGameInput;
    [SerializeField] private InputField JoinGameInput;
    [SerializeField] private GameObject StartButton;

    private void Awake()
    {
        var appSettings = new Photon.Realtime.AppSettings();
        appSettings.AppIdRealtime = "6244a3da-e912-4b7f-b267-492a9767477f";
        appSettings.AppIdChat = "your_chat_app_id";
        appSettings.UseNameServer = true;

        PhotonNetwork.ConnectUsingSettings(appSettings);
    }

    private void Start()
    {
        if (UsernameMenu != null)
        {
            UsernameMenu.SetActive(true);
        }
        else
        {
            Debug.LogError("UsernameMenu is not assigned in the inspector!");
        }

        Debug.Log("Start method completed. UsernameMenu: " + UsernameMenu);
    }

    public void ChangeUserNameInput()
    {
        if (UsernameInput.text.Length >= 4)
        {
            StartButton.SetActive(true);
        }
        else
        {
            StartButton.SetActive(false);
        }
    }

    public void SetUserName()
    {
        if (UsernameMenu != null)
        {
            UsernameMenu.SetActive(false);
        }
        else
        {
            Debug.LogError("UsernameMenu is not assigned in the inspector!");
        }

        PhotonNetwork.NickName = UsernameInput.text;
    }

    public void CreateGame()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;

        PhotonNetwork.CreateRoom(CreateGameInput.text, roomOptions, null);
    }

    public void JoinGame()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 5;

            PhotonNetwork.JoinOrCreateRoom(JoinGameInput.text, roomOptions, TypedLobby.Default);
        }
        else
        {
            Debug.LogError("Client not ready to create or join a room. Wait for OnConnectedToMaster or join a lobby first.");
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server!");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Stage1");
    }
}
