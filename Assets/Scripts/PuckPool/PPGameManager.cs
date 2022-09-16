using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PPGameManager : MonoBehaviourPunCallbacks
{
    public Transform StrikerSpwanPoint;
    private List<string> _strikerList;
    private List<string> _oppoStrikerList;
    public Vector2[] PlayerSpawnPoints;
    public Vector2[] OpponentSpawnPoints;
    [SerializeField] private GameObject Striker;
    [SerializeField] private GameObject OppoStriker;
    public bool IsGreen;
    public bool IsPassNPlay;
    public static PPGameManager Instance;

    private void Start()
    {
        if (Instance == null)
            Instance = this;

        _strikerList = new List<string>();
        _oppoStrikerList = new List<string>();
    }

    public override void OnJoinedRoom()
    {
        IsPassNPlay = false;
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            SpawnStrikers(PlayerSpawnPoints);
            IsGreen = true;
        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            OppoSpawnStrikers(OpponentSpawnPoints);
            IsGreen = false;
        }
    }

    public void SpawnStrikers(Vector2[] positions)
    {
        if (PhotonNetwork.InRoom)
        {
            foreach (var pos in positions)
            {
                var prefab = PhotonNetwork.Instantiate(Striker.name, pos, Quaternion.identity);
                prefab.name = $"Striker_{pos}";
            }

        }
        else
        {
            foreach (var pos in positions)
            {
                var prefab = Instantiate(Striker, StrikerSpwanPoint);
                prefab.transform.SetPositionAndRotation(pos, Quaternion.identity);
                prefab.name = $"Striker_{pos}";
            }
        }
    }

    public void OppoSpawnStrikers(Vector2[] positions)
    {
        if (PhotonNetwork.InRoom)
        {
            foreach (var pos in positions)
            {
                var prefab = PhotonNetwork.Instantiate(OppoStriker.name, pos, Quaternion.identity);
                prefab.name = $"OppoStriker_{pos}";
            }
        }
        else
        {
            foreach (var pos in positions)
            {
                var prefab = Instantiate(OppoStriker, StrikerSpwanPoint);
                prefab.transform.SetPositionAndRotation(pos, Quaternion.identity);
                prefab.name = $"OppoStriker_{pos}";
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Strikers"))
        {
            if (collision.transform.position.y > 0 && !_strikerList.Contains(collision.gameObject.name))
            {
                _strikerList.Add(collision.gameObject.name);
                ScoreController.Instance.Player1Scored(_strikerList.Count);
            }
            else if (collision.transform.position.y < 0 && _strikerList.Contains(collision.gameObject.name))
            {
                _strikerList.Remove(collision.gameObject.name);
                ScoreController.Instance.Player1Scored(_strikerList.Count);
            }

        }
        else if (collision.CompareTag("OppoStrikers"))
        {
            if (collision.transform.position.y < 0 && !_oppoStrikerList.Contains(collision.gameObject.name))
            {
                _oppoStrikerList.Add(collision.gameObject.name);
                ScoreController.Instance.Player2Scored(_oppoStrikerList.Count);
            }
            else if (collision.transform.position.y > 0 && _oppoStrikerList.Contains(collision.gameObject.name))
            {
                _oppoStrikerList.Remove(collision.gameObject.name);
                ScoreController.Instance.Player2Scored(_oppoStrikerList.Count);
            }
        }
    }
}

