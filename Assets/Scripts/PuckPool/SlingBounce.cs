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
                if (collision.transform.tag == "Strikers"  )
                {
                    TouchObjects touchObjects = _touchObjects.Find(touch => touch.fingerID == _touch.fingerId);
                    if (_touch.phase == TouchPhase.Ended && _touch.position.y < Screen.height / 2 && touchObjects.selectedItem == collision.gameObject)
                    {
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * BounceForce, ForceMode2D.Impulse);
                    }
                }
                else if (collision.transform.tag == "OppoStrikers")
                {
                    TouchObjects opptouchObjects = _touchObjects.Find(touch => touch.fingerID == _touch.fingerId);
                    if (_touch.phase == TouchPhase.Ended && _touch.position.y > Screen.height / 2 && opptouchObjects.selectedItem == collision.gameObject)
                    {
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(-Vector2.up * BounceForce, ForceMode2D.Impulse);
                    }
                }
            }

        }
    }
}
