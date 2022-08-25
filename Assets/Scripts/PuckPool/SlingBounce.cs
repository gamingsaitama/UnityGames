using UnityEngine;

public class SlingBounce : MonoBehaviour
{
    public float BounceForce;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Input.touchCount > 0)
        {
            foreach (var _touch in Input.touches)
            {
                if (collision.transform.tag == "Strikers")
                {
                    if (_touch.phase == TouchPhase.Ended && _touch.position.y < Screen.height / 2)
                    {
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * BounceForce, ForceMode2D.Impulse);
                    }
                }
                else if (collision.transform.tag == "OppoStrikers")
                {
                    if (_touch.phase == TouchPhase.Ended && _touch.position.y > Screen.height / 2)
                    {
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(-Vector2.up * BounceForce, ForceMode2D.Impulse);
                    }
                }
            }

        }
    }
}
