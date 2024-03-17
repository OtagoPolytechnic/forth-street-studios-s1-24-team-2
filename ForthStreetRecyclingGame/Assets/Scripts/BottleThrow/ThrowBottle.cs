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
    [Header("Components")]
    public Rigidbody rb;              //Object rb for Throw() force calculation
    public LineRenderer lr;           //Used to show Throw() trajectory
    public Camera mainCamera;         //Scene camera used to limit object throw angle

    [Header("Throw Variables")]
    public Vector3 mouseInitialPos;   //Mouse position when clicking object
    public Vector3 mouseReleasePos;   //Mouse position when releasing click
    public bool bottleThrown;         //Changes after object is thrown
    public float forceMultiplier = 3; //Added in Throw() force calculation

    /// <summary>
    /// Instantiate scene objects to variables
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
        mainCamera = Camera.main; //Likely need to change when we finalise loading of minigames in the main scene
        lr.enabled = false; //Disable any line from object before clicking
    }

    /// <summary>
    /// Save mouse position when clicking on gameobject (as Vector3) <br />
    /// Enable the line renderer and update with initial line position
    /// </summary>
    private void OnMouseDown()
    {
        mouseInitialPos = Input.mousePosition;
        lr.enabled = true; //Show line only while clicking

        UpdateLineRenderer();
    }

    /// <summary>
    /// Update line renderer positions during mouse drag
    /// </summary>
    private void OnMouseDrag()
    {
        UpdateLineRenderer();
    }

    /// <summary>
    /// Triggers throwing action if mouse is dragged downward, launching the object away from the camera.
    /// </summary>
    private void OnMouseUp()
    {
        lr.enabled = false; //Disable trajectory line on release

        mouseReleasePos = Input.mousePosition; //set to centre of object instead of mouse position within object

        if (mouseReleasePos.y < mouseInitialPos.y) // Ensure the mouse is dragged downward so object launches away from camera
        {
            Throw(mouseInitialPos - mouseReleasePos);
            rb.useGravity = true;
        }
    }

    void Throw(Vector3 force)
    {
        if (bottleThrown) { return; } //Only able to throw object once

        // Calculate angle between camera forward direction and mouse drag direction
        Vector3 camForward = mainCamera.transform.forward;
        Vector3 mouseDragDir = force.normalized;
        float angle = Vector3.Angle(camForward, mouseDragDir);

        // Check if the angle is within the bottom 180 degrees
        if (angle <= 90)
        {
            rb.AddForce(new Vector3(force.x, force.y, force.y) * forceMultiplier);
            bottleThrown = true;
        }
    }

    /// <summary>
    /// Draws a line from the click position to the current mouse position,<br />
    /// Help from ChatGPT for this (OnMouse events code sent along with prompt.),<br />
    /// PROMPT: I need this function to draw a line from mouseInitialPos to the current Input.mousePosition while the mouse is clicked down
    /// </summary>
    void UpdateLineRenderer()
    {
        // Set the positions of the line renderer from mouseInitialPos to current mouse position
        Vector3[] positions = new Vector3[2];
        positions[0] = mainCamera.ScreenToWorldPoint(new Vector3(mouseInitialPos.x, mouseInitialPos.y, 10));
        positions[1] = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        lr.SetPositions(positions);
    }
}