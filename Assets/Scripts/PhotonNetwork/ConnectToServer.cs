using UnityEngine;
using Photon.Pun;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
   
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); 
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        Debug.Log("Connected to master: "+ PhotonNetwork.IsMasterClient);
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("Connected Lobby...");
    }
   
}
