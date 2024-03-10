//Tutorial used for final help: https://www.youtube.com/watch?v=99yIg-A5eCw
// Modified the direction of ball launch (away instead of towards camera)

/*      FEATURES TO ADD
 *      ---------------
 *  1.1) Line renderer showing throw strength (towards mouse)
 *       or
 *  1.2) Line renderer showing throw trajectory (away from object)
 *  2.1) Different items get thrown with differing force
 *  3.1) Limit throw angle 
 *      -  bottom 180° of object - remove throwing towards camera
 *  4.1) What to do if throw is too weak to get near trigger area
 */

using UnityEngine;

public class ThrowBottle : MonoBehaviour
{
    private Vector3 mouseInitialPos;
    private Vector3 mouseReleasePos;
    private Rigidbody rb;
    private bool bottleThrown;
    private float forceMultiplier = 3;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        mouseInitialPos = Input.mousePosition;
    }

    private void OnMouseUp()
    {
        mouseReleasePos = Input.mousePosition;
        Throw(mouseInitialPos - mouseReleasePos);
        rb.useGravity = true;
    }

    void Throw(Vector3 Force)
    {
        if (bottleThrown) { return; }
        rb.AddForce(new Vector3(Force.x, Force.y, Force.y) * forceMultiplier);
        bottleThrown = true;
    }
}