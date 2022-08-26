using System;
using System.Collections.Generic;
using UnityEngine;

public class TouchMoveStrikers : MonoBehaviour
{
    RaycastHit2D hit;
    Vector2[] touches = new Vector2[10];

    protected List<TouchObjects> _touchObjects = new List<TouchObjects>();
    protected List<TouchObjects> _opptouchObjects = new List<TouchObjects>();

    void Update()
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

                        if (touchObj != null && touchObj.selectedItem.transform.position.y < -0.4 && touchObj.selectedItem.CompareTag("Strikers"))//_touchObjects.Count > 0 &&
                        {
                            touchObj.selectedItem.transform.position = new Vector2(Mathf.Clamp(touches[t.fingerId].x, -1.78f, 1.78f),
                                                            Mathf.Clamp(touches[t.fingerId].y, -3.54f, -0.6f));
                        }
                        else if (_opptouchObjects != null && opptouchObj.selectedItem.transform.position.y > 0.4 && opptouchObj.selectedItem.CompareTag("OppoStrikers"))//_opptouchObjects.Count > 0 && 
                        {
                            opptouchObj.selectedItem.transform.position = new Vector2(Mathf.Clamp(touches[t.fingerId].x, -1.78f, 1.78f),
                                                       Mathf.Clamp(touches[t.fingerId].y, 0.6f, 3.54f));
                        }
                    }
                    else if (Input.GetTouch(t.fingerId).phase == TouchPhase.Ended)
                    {
                        if (_touchObjects.Count > 0)
                        {
                            TouchObjects touchObj = _touchObjects.Find(touch => touch.fingerID == t.fingerId);
                            _touchObjects.RemoveAt(_touchObjects.IndexOf(touchObj));
                        }
                        else if (_opptouchObjects.Count > 0)
                        {
                            TouchObjects opptouchObj = _opptouchObjects.Find(touch => touch.fingerID == t.fingerId);
                            _opptouchObjects.RemoveAt(_opptouchObjects.IndexOf(opptouchObj));
                        }
                        _touchObjects.Clear();
                        _opptouchObjects.Clear();
                    }
                }
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