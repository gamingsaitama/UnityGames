using UnityEngine;

public class SlingBounce : MonoBehaviour
{
    public float BounceForce;
    private bool _canLaunchStriker = false;
    private bool _canLaunchOppoStriker = false;

    private void OnCollisionStay2D(Collision2D collision)
     {
         if (collision.transform.tag == "Strikers")
         {
            _canLaunchStriker = true;
            if (Input.touchCount > 0)
            {
                Touch _touch = Input.GetTouch(0);
                if (_touch.phase == TouchPhase.Ended && _touch.position.y < Screen.height/2 && _canLaunchStriker) 
                {
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * BounceForce, ForceMode2D.Impulse);
                    _canLaunchStriker = false;
                }
            }
         }
        else if (collision.transform.tag == "OppoStrikers" )
        {
            _canLaunchOppoStriker = true;
            if (Input.touchCount > 0)
            {
                Touch _touch = Input.GetTouch(0);
                if (_touch.phase == TouchPhase.Ended && _touch.position.y > Screen.height / 2 && _canLaunchOppoStriker)
                {
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(-Vector2.up * BounceForce, ForceMode2D.Impulse);
                    _canLaunchOppoStriker = false;
                }
            }
        }
     }
}
