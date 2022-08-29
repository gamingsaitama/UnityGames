using UnityEngine;

public class SlingBounce : TouchMoveStrikers
{
    public float BounceForce;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch _touch in Input.touches)
            {
                if (collision.transform.tag == "Strikers")
                {
                    TouchObjects touchObjects = _touchObjects.Find(touch => touch.fingerID == _touch.fingerId);
                    if (touchObjects != null && _touch.phase == TouchPhase.Ended && _touch.position.y < Screen.height / 2 && collision.gameObject == touchObjects.selectedItem)
                    {
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * BounceForce, ForceMode2D.Impulse);
                    }
                }
                else if (collision.transform.tag == "OppoStrikers")
                {
                    TouchObjects opptouchObjects = _opptouchObjects.Find(touch => touch.fingerID == _touch.fingerId);
                    if (opptouchObjects != null && _touch.phase == TouchPhase.Ended && _touch.position.y > Screen.height / 2 && collision.gameObject == opptouchObjects.selectedItem)
                    {
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(-Vector2.up * BounceForce, ForceMode2D.Impulse);
                    }
                }
            }

        }
    }
}
