using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPGameManager : MonoBehaviour
{
    public Transform StrikerSpwanPoint;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       
    }
}

