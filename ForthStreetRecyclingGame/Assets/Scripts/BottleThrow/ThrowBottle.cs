//Tutorial used for final help: https://www.youtube.com/watch?v=99yIg-A5eCw
// Modified the direction of ball launch (away instead of towards camera)

/*      FEATURES TO ADD                                                     STATUS
 *      ---------------                                                     ------
 *  1.1) Line renderer showing throw strength (towards mouse)
 *       or
 *  1.2) Line renderer showing throw trajectory (away from object)
 *  2.1) Different items get thrown with differing force
 *  3.1) Limit throw angle                                                  DONE
 *      -  bottom 180° of object - remove throwing towards camera           DONE
 *  4.1) What to do if throw is too weak to get near trigger area
 */

using UnityEngine;

public class ThrowBottle : MonoBehaviour
{
    private Vector3 mouseInitialPos;   //Mouse position when clicking object
    private Camera mainCamera;         //Scene camera used to limit object throw angle
    private Rigidbody rb;              //Object rb for Throw() force calculation
    private bool bottleThrown;         //Changes after object is thrown
    private float forceMultiplier = 3; //Added in Throw() force calculation

    /// <summary>
    /// Instantiate scene objects to variables
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main; //Likely need to change when we finalise loading of minigames in the main scene
    }

    /// <summary>
    /// Save mouse position when clicking on gameobject (as Vector3)
    /// </summary>
    private void OnMouseDown()
    {
        mouseInitialPos = Input.mousePosition;
    }

    /// <summary>
    /// Triggers throwing action if mouse is dragged downward, launching the object away from the camera.
    /// </summary>
    private void OnMouseUp()
    {
        Vector3 mouseReleasePos = Input.mousePosition;

        if (mouseReleasePos.y < mouseInitialPos.y) // Ensure the mouse is dragged downward so object launches away from camera
        {
            Throw(mouseInitialPos - mouseReleasePos);
            rb.useGravity = true;
        }
    }

    void Throw(Vector3 force)
    {
        if (bottleThrown) { return; }

        // Calculate angle between camera forward direction and mouse drag direction
        Vector3 camForward = mainCamera.transform.forward;
        Vector3 mouseDragDir = (mouseInitialPos - Input.mousePosition).normalized;
        float angle = Vector3.Angle(camForward, mouseDragDir);

        // Check if the angle is within the bottom 180 degrees
        if (angle <= 90)
        {
            // Apply force only along the X and Z axes
            rb.AddForce(new Vector3(force.x, force.y, force.y) * forceMultiplier);
            bottleThrown = true;
        }
    }
}