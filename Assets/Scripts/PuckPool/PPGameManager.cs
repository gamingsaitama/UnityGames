using System.Collections.Generic;
using UnityEngine;

public class PPGameManager : MonoBehaviour
{
    public Transform StrikerSpwanPoint;
    private int _scoreStriker;
    private int _scoreOppoStriker;
    private List<GameObject> _strikerList;
    private List<GameObject> _oppoStrikerList;
    [SerializeField]private Vector2[] PlayerSpawnPoints;
    [SerializeField] private Vector2[] OpponentSpawnPoints;
    [SerializeField] private GameObject Striker;
    [SerializeField] private GameObject OppoStriker;

    private void Start()
    {
        SpawnStrikers(PlayerSpawnPoints);
        OppoSpawnStrikers(OpponentSpawnPoints);
    }

    private void SpawnStrikers(Vector2[] positions)
    {
        foreach (var pos in positions)
        {
            var prefab = Instantiate(Striker, StrikerSpwanPoint);
            prefab.transform.SetPositionAndRotation(pos, Quaternion.identity);
        }
    }

    private void OppoSpawnStrikers(Vector2[] positions)
    {
        foreach (var pos in positions)
        {
            var prefab = Instantiate(OppoStriker, StrikerSpwanPoint);
            prefab.transform.SetPositionAndRotation(pos, Quaternion.identity);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Strikers") && _strikerList.Contains(collision.gameObject))
        {
            if (collision.transform.position.y > Screen.height / 2)
            {
                _scoreStriker++;
                _strikerList.Add(collision.gameObject);
            }
            else
            {
                _scoreStriker--;
                _strikerList.Remove(collision.gameObject);
            }

        }
        else if (collision.collider.CompareTag("OppoStrikers") && _oppoStrikerList.Contains(collision.gameObject))
        {
            if (collision.transform.position.y < Screen.height / 2)
            {
                _scoreOppoStriker++;
                _oppoStrikerList.Add(collision.gameObject);
            }
            else
            {
                _scoreOppoStriker--;
                _oppoStrikerList.Remove(collision.gameObject);
            }
        }
    }

    public void ResetScore()
    {
        _strikerList = new List<GameObject>();
        _oppoStrikerList = new List<GameObject>();
        _scoreStriker = 0;
        _scoreOppoStriker = 0;
    }
}

