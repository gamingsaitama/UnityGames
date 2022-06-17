using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BallMovement : MonoBehaviour
{
    [Tooltip("Add more waypoints to move the capsule")]
    [Header("Ball WayPoint")]
    public List<BallMovementWaypoints> _wayPoints;
    [Space]

    [SerializeField] int _currentWayPoint = 0;
    [SerializeField] float _objectSpeed;
    [SerializeField] ParticleSystem _particleSystem;
    [Space]
    [Header("Colors")]
    [SerializeField] List<Color> _colors;
    MeshRenderer _playerMeshRenderer;

    [Space]
    [Header("AudioClip")]
    [SerializeField] AudioClip _killClip;
    [SerializeField] AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _playerMeshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
             MoveInWayPoints();
        }
    }

    private void MoveInWayPoints()
    {

        transform.position = Vector3.MoveTowards(transform.position, _wayPoints[_currentWayPoint].pointPos, Time.deltaTime * _objectSpeed);

        if(_wayPoints[_currentWayPoint].canRotate)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.EulerAngles(_wayPoints[_currentWayPoint].pointRotation.x, _wayPoints[_currentWayPoint].pointRotation.y, _wayPoints[_currentWayPoint].pointRotation.z), Time.deltaTime * _objectSpeed);

        _playerMeshRenderer.material.color = _colors[_currentWayPoint];

        if(transform.position == _wayPoints[_currentWayPoint].pointPos)
        {
            if(_currentWayPoint == _wayPoints.Count - 1)
            {
                _playerMeshRenderer.material.color = Color.white;

                _currentWayPoint = 0;
            }
            else
            {
                if(_currentWayPoint < _wayPoints.Count - 1)
                {
                    _currentWayPoint++;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "kill")
        {
            _playerMeshRenderer.material.color = Color.magenta;
            var killparticle = SpawnParticleEffects(other.gameObject);
            other.gameObject.SetActive(false);

            Destroy(killparticle, 0.2f);
            Destroy(killparticle, 0.3f);

        }
    }

    GameObject SpawnParticleEffects(GameObject other)
    {
        ParticleSystem particleSystem = Instantiate(_particleSystem, other.transform.position, Quaternion.identity);
        PlayAudio(_killClip);
        particleSystem.Play();
        return particleSystem.gameObject;
    }

    void PlayAudio(AudioClip audioClip)
    {
        _audioSource.clip = audioClip;
        _audioSource.Play();
    }
}


[Serializable]
public class BallMovementWaypoints
{
    public Vector3 pointPos;
    public Vector3 pointRotation;
    public bool canRotate;
}