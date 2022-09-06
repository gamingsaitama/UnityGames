using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using System.Collections;

public class PPGameModeManager : MonoBehaviourPunCallbacks
{

    public GameObject LoadingPanel;
    public GameObject FriendsPanel;
    public GameObject GameModePanel;
    public Button Friend, PassNPlay, Online, FriendsCreateRoom, FriendsJoinRoom,BackButton;
    private TypedLobby _friendsLobby;
    private TypedLobby _onlineLobby;
    private static int _maxPlayer = 2;
    public static PPGameModeManager Instance;

    private void Awake()
    {
        if (Instance != null)
            return;
        Instance = this;
    }

    private void Start()
    {
        LoadingPanel.SetActive(true);
        _friendsLobby = new TypedLobby("Friends", LobbyType.Default);
        _onlineLobby = new TypedLobby("Online", LobbyType.Default);
        Friend.onClick.AddListener(OnClickFriendsName);
        PassNPlay.onClick.AddListener(OnClickPassNPlay);
        Online.onClick.AddListener(OnClickOnlineMode);
        FriendsCreateRoom.onClick.AddListener(CreateFriendsRoom);
        FriendsJoinRoom.onClick.AddListener(JoinFriendsRoom);
        BackButton.onClick.AddListener(OnClickBackButton);
        if (PhotonNetwork.InLobby)
        {
            LoadingPanel.SetActive(false);
        }
    }

    private RoomOptions SetRoomProps()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.PublishUserId = true;
        roomOptions.MaxPlayers = (byte)_maxPlayer;
        roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();
        return roomOptions;
    }

    private void OnClickOnlineMode()
    {
        if (PhotonNetwork.JoinLobby(_onlineLobby))
        {
            StartOnlinemode();
        }
        else
        {
            Debug.Log($"Joining lobby failed");
        }
    }

    private void StartOnlinemode()
    {
        GameModePanel.SetActive(false);
        LoadingPanel.SetActive(true);
        RoomOptions roomOptions = SetRoomProps();
        roomOptions.CustomRoomProperties.Add("type", _onlineLobby.Name);
        if (PhotonNetwork.CountOfRooms > 0)
        {
            JoiningRoom(null, roomOptions, _onlineLobby);
        }
        else
        {
            RoomCreation(roomOptions, _onlineLobby);
        }
    }

    private void RoomCreation(RoomOptions roomOptions, TypedLobby lobbyType, string roomName = null)
    {
        PhotonNetwork.CreateRoom(roomName, roomOptions, lobbyType);
    }

    private void JoiningRoom(string roomName, RoomOptions roomOptions = null, TypedLobby lobbyType = null)
    {
        if (lobbyType != null)
        {
            PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, lobbyType);
        }
        else
        {
            PhotonNetwork.JoinRoom(roomName);
        }
    }

    private void OnClickFriendsName()
    {
        if (PhotonNetwork.JoinLobby(_friendsLobby))
        {
            GameModePanel.SetActive(false);
            FriendsPanel.SetActive(true);
        }
        else
        {
            Debug.Log($"Joining lobby failed");
        }
    }

    private void CreateFriendsRoom()
    {
        LoadingPanel.SetActive(true);
        FriendsPanel.SetActive(false);
        RoomOptions roomOptions = SetRoomProps();
        roomOptions.CustomRoomProperties.Add("type", _friendsLobby.Name);
        string id = GenerateRoomId(_friendsLobby.Name + "_");
        UIManager.Instance.ShowRoomNumber(id.Substring(8));
        RoomCreation(roomOptions, _friendsLobby, id);
    }

    private void JoinFriendsRoom()
    {
        string id = UIManager.Instance.GetRoomNumber();
        if (!string.IsNullOrEmpty(id))
        {
            LoadingPanel.SetActive(true);
            JoiningRoom(_friendsLobby.Name + id);
            FriendsPanel.SetActive(false);
        }
        else
        {
            Debug.Log("not valid roomid");
            StartCoroutine(DisplayMessage("not valid roomid"));
        }
    }

    private void OnClickPassNPlay()
    {
        GameModePanel.SetActive(false);
    }

    private string GenerateRoomId(string type)
    {
        int id = Random.Range(10000, 99999);
        return type + id;
    }

    private IEnumerator DisplayMessage(string message)
    {
        LoadingPanel.SetActive(true);
        UIManager.Instance.DisplayMessage(message);
        yield return new WaitForSeconds(3f);
        LoadingPanel.SetActive(false);
    }

    public void LeaveRoomAndLobby()
    {
        if (PhotonNetwork.CurrentRoom != null)
            PhotonNetwork.LeaveRoom();
        if (PhotonNetwork.CurrentLobby != null)
            PhotonNetwork.LeaveLobby();
    }

    private void OnClickBackButton()
    {
        GameModePanel.SetActive(true);
        FriendsPanel.SetActive(false);
    }

    #region PhotonCallBacks

    public override void OnJoinedRoom()
    {
        Debug.Log($".....");
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            Debug.Log("i am the new player...");
            LoadingPanel.SetActive(false);
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }

    public override void OnJoinedLobby()
    {
        LoadingPanel.SetActive(false);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room created " + PhotonNetwork.IsMasterClient);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"..... {PhotonNetwork.PlayerListOthers[0].UserId}");
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            Debug.Log("new player have joined the room...");
            LoadingPanel.SetActive(false);
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log($"{returnCode} .. {message}..");
        GameModePanel.SetActive(true);
        StartCoroutine(DisplayMessage(message));
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"{returnCode} .. {message} .. Randomroom");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("player left room... " + otherPlayer.NickName);
    }
    #endregion
}
