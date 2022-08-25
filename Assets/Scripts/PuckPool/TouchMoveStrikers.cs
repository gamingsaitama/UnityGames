using System;
using System.Collections.Generic;
using UnityEngine;

public class TouchMoveStrikers : MonoBehaviour
{
    RaycastHit2D hit;
    Vector2[] touches = new Vector2[10];

    List<TouchObjects> touchObjects = new List<TouchObjects>();

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

                        if (hit)
                        {
                            touchObjects.Add(new TouchObjects(hit.transform.gameObject, t.fingerId));
                        }
                    }
                    else if (Input.GetTouch(t.fingerId).phase == TouchPhase.Moved)
                    {
                        TouchObjects touchObj = touchObjects.Find(touch => touch.fingerID == t.fingerId);
                        if (touches[t.fingerId].y < -0.4 && touchObj.selectedItem.CompareTag("Strikers"))
                        {
                            touchObj.selectedItem.transform.position = new Vector2(Mathf.Clamp(touches[t.fingerId].x, -1.78f, 1.78f),
                                                            Mathf.Clamp(touches[t.fingerId].y, -3.54f, -0.6f));
                        }

                        else if (touches[t.fingerId].y > 0.4 && touchObj.selectedItem.CompareTag("OppoStrikers"))
                        {
                            touchObj.selectedItem.transform.position = new Vector2(Mathf.Clamp(touches[t.fingerId].x, -1.78f, 1.78f),
                                                       Mathf.Clamp(touches[t.fingerId].y, 0.6f, 3.54f));
                        }
                    }
                    else if (Input.GetTouch(t.fingerId).phase == TouchPhase.Ended)
                    {
                        TouchObjects touchObj = touchObjects.Find(touch => touch.fingerID == t.fingerId);
                        touchObjects.RemoveAt(touchObjects.IndexOf(touchObj));
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