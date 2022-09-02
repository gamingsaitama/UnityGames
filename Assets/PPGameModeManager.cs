using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PPGameModeManager : MonoBehaviourPunCallbacks
{

    public GameObject LoadingPanel;
    public GameObject FriendsPanel;
    public GameObject GameModePanel;
    public Button Friend, PassNPlay, Online, FriendsCreateRoom, FriendsJoinRoom;
    private TypedLobby _friendsLobby;
    private TypedLobby _onlineLobby;
    private static int _maxPlayer = 2;

    private void Start()
    {
        LoadingPanel.SetActive(true);
        _friendsLobby = new TypedLobby("Friends",LobbyType.Default);
        _onlineLobby = new TypedLobby("Online",LobbyType.Default);
        Friend.onClick.AddListener(() => OnClickFriendsName());
        PassNPlay.onClick.AddListener(() => OnClickPassNPlay());
        Online.onClick.AddListener(() => OnClickOnlineMode());
        FriendsCreateRoom.onClick.AddListener(() => CreateFriendsRoom());
        FriendsJoinRoom.onClick.AddListener(() => JoinFriendsRoom());
    }

    private RoomOptions SetRoomProps()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = false;
        roomOptions.PublishUserId = true;
        roomOptions.MaxPlayers = (byte)_maxPlayer;
        return roomOptions;
    }

    public void OnClickOnlineMode()
    {
        GameModePanel.SetActive(false);
        LoadingPanel.SetActive(true);
        UIManager.Instance.JoinRoomField.gameObject.SetActive(false);
        RoomOptions roomOptions = SetRoomProps();
        PhotonNetwork.JoinRandomOrCreateRoom(expectedMaxPlayers:2,matchingType:MatchmakingMode.FillRoom,typedLobby:_onlineLobby,roomOptions:roomOptions);
    }

    public void OnClickFriendsName()
    {
        GameModePanel.SetActive(false);
        FriendsPanel.SetActive(true);
    }

    private void CreateFriendsRoom()
    {
        LoadingPanel.SetActive(true);
        FriendsPanel.SetActive(false);
        RoomOptions roomOptions = SetRoomProps();
        string id = GenerateRoomId(_friendsLobby.Name + "_");
        UIManager.Instance.ShowRoomNumber(id.Substring(8));
        PhotonNetwork.CreateRoom(id, roomOptions, _friendsLobby);
    }

    public void JoinFriendsRoom()
    {
        string id = UIManager.Instance.GetRoomNumber();
        if (!string.IsNullOrEmpty(id))
        {
            LoadingPanel.SetActive(true);
            PhotonNetwork.JoinRoom(_friendsLobby.Name+id);
            FriendsPanel.SetActive(false);
        }
        else
        {
            Debug.Log("not valid roomid");
        }
    }

    public void OnClickPassNPlay()
    {
        GameModePanel.SetActive(false);
    }

    private string GenerateRoomId(string type)
    {
        int id = Random.Range(10000,99999);
        return type + id;
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(".....");
    }

    public override void OnJoinedLobby()
    {
        LoadingPanel.SetActive(false);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            LoadingPanel.SetActive(false);
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }

}
