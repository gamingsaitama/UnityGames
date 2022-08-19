using System.Collections.Generic;
using UnityEngine;

public class PPGameManager : MonoBehaviour
{
    public Transform StrikerSpwanPoint;
    private List<string> _strikerList;
    private List<string> _oppoStrikerList;
    [SerializeField] private Vector2[] PlayerSpawnPoints;
    [SerializeField] private Vector2[] OpponentSpawnPoints;
    [SerializeField] private GameObject Striker;
    [SerializeField] private GameObject OppoStriker;

    private void Start()
    {
        SpawnStrikers(PlayerSpawnPoints);
        OppoSpawnStrikers(OpponentSpawnPoints);
        _strikerList = new List<string>();
        _oppoStrikerList = new List<string>();
    }

    private void SpawnStrikers(Vector2[] positions)
    {
        foreach (var pos in positions)
        {
            var prefab = Instantiate(Striker, StrikerSpwanPoint);
            prefab.transform.SetPositionAndRotation(pos, Quaternion.identity);
            prefab.name = $"Striker_{pos}";
        }
    }

    private void OppoSpawnStrikers(Vector2[] positions)
    {
        foreach (var pos in positions)
        {
            var prefab = Instantiate(OppoStriker, StrikerSpwanPoint);
            prefab.transform.SetPositionAndRotation(pos, Quaternion.identity);
            prefab.name = $"OppoStriker_{pos}";
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Strikers") )
        {
            if (collision.transform.position.y > 0 && !_strikerList.Contains(collision.gameObject.name))
            {
                _strikerList.Add(collision.gameObject.name);
                ScoreController.Instance.Player1Scored(_strikerList.Count);
            }
            else if(collision.transform.position.y < 0 && _strikerList.Contains(collision.gameObject.name))
            {
                _strikerList.Remove(collision.gameObject.name);
                ScoreController.Instance.Player1Scored(_strikerList.Count);
            }

        }
        else if (collision.CompareTag("OppoStrikers") )
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

