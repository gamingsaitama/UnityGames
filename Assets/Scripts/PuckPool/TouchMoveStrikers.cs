using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class TouchMoveStrikers : MonoBehaviour
{
    RaycastHit2D hit;
    Vector2[] touches = new Vector2[10];

    protected List<TouchObjects> _touchObjects = new List<TouchObjects>();
    protected List<TouchObjects> _opptouchObjects = new List<TouchObjects>();
    private PhotonView View;

    private void Start()
    {
        View = GetComponent<PhotonView>();
    }
    void Update()
    {
        if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount==2)
        {
            if (View.IsMine)
            {
                MoveStrikers();
            }
        }
        else
        {
            MoveStrikers();
        }
    }

    private void MoveStrikers()
    {
        try
        {
            if (Input.touchCount > 0)
            {
                foreach (Touch t in Input.touches)
                {
                    touches[t.fingerId] = Camera.main.ScreenToWorldPoint(Input.GetTouch(t.fingerId).position);
                    if (Input.GetTouch(t.fingerId).phase == TouchPhase.Began)
                    {
                        hit = Physics2D.Raycast(touches[t.fingerId], Vector2.zero);
                        if (hit.collider != null)
                        {
                            if (hit.collider.CompareTag("Strikers"))
                            {
                                _touchObjects.Add(new TouchObjects(hit.transform.gameObject, t.fingerId));
                            }
                            else if (hit.collider.CompareTag("OppoStrikers"))
                            {
                                _opptouchObjects.Add(new TouchObjects(hit.transform.gameObject, t.fingerId));
                            }
                        }
                    }
                    else if (Input.GetTouch(t.fingerId).phase == TouchPhase.Moved)
                    {
                        TouchObjects touchObj = _touchObjects.Find(touch => touch.fingerID == t.fingerId);
                        TouchObjects opptouchObj = _opptouchObjects.Find(touch => touch.fingerID == t.fingerId);

                        if (touchObj != null && touchObj.selectedItem.transform.position.y < -0.1 && touchObj.selectedItem.CompareTag("Strikers"))
                        {
                            touchObj.selectedItem.transform.position = new Vector2(Mathf.Clamp(touches[t.fingerId].x, -1.78f, 1.78f),
                                                            Mathf.Clamp(touches[t.fingerId].y, -3.54f, -0.6f));
                        }
                        else if (opptouchObj != null && opptouchObj.selectedItem.transform.position.y > 0.1 && opptouchObj.selectedItem.CompareTag("OppoStrikers"))
                        {
                            opptouchObj.selectedItem.transform.position = new Vector2(Mathf.Clamp(touches[t.fingerId].x, -1.78f, 1.78f),
                                                       Mathf.Clamp(touches[t.fingerId].y, 0.6f, 3.54f));
                        }
                    }
                    else if (Input.GetTouch(t.fingerId).phase == TouchPhase.Ended || Input.GetTouch(t.fingerId).phase == TouchPhase.Canceled)
                    {
                        TouchObjects touchObj = _touchObjects.Find(touch => touch.fingerID == t.fingerId);
                        if (touchObj != null)
                        {
                            _touchObjects.RemoveAt(_touchObjects.IndexOf(touchObj));
                        }
                        TouchObjects opptouchObj = _opptouchObjects.Find(touch => touch.fingerID == t.fingerId);
                        if (opptouchObj != null)
                        {
                            _opptouchObjects.RemoveAt(_opptouchObjects.IndexOf(opptouchObj));
                        }

                    }
                }
            }
            else
            {
                _touchObjects.Clear();
                _opptouchObjects.Clear();
                touches = new Vector2[10];
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    [Serializable]
    public class TouchObjects
    {
        public GameObject selectedItem;
        public int fingerID;
        public Vector2 initPos;

        public TouchObjects(GameObject objSelected, int newFingerId)
        {
            fingerID = newFingerId;
            selectedItem = objSelected;
            initPos = objSelected.transform.position;
        }
    }

}