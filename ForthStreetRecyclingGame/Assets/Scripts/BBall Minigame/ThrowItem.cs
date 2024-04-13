//Tutorials used for help with:
//     Initial object throwing: https://www.youtube.com/watch?v=99yIg-A5eCw Only used as a starting point for Throw()
//          Initial trajectory: https://www.youtube.com/watch?v=9iwdAMXydgo Some code in UpdateTrajectory()
//       Stopping on collision: https://www.youtube.com/watch?v=13KrnisMf14 Modified how this works further in UpdateTrajectory()

using System.Collections.Generic;
using UnityEngine;

public class ThrowItem : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody rb; //Object rb for Throw() force calculation
    private LineRenderer lr; //Used to show Throw() trajectory
    private Camera mainCamera; //Scene camera used to limit object throw angle
    private GameManager manager; //GameManager script in scene
    private SpawnerManager spawnerManager; //SpawnerManager script in scene 

    [Header("Throw Variables")]
    private Vector3 mouseInitialPos; //Mouse position when clicking object
    private Vector3 mouseReleasePos; //Mouse position when releasing click
    private bool bottleThrown; //Changes after object is thrown
    private float forceMultiplier; //Added in Throw() force calculation

    [Header("Line Renderer Variables")]
    private List<Vector3> linePoints; // List to store points along the trajectory line
    private int lineSegmentCount; // Number of segments used to draw the trajectory line
    private float maxTrajectoryLength; // Maximum length of the trajectory line 
    private Vector3 mouseInitialWorldPos;

    /// <summary>
    /// Instantiate scene objects to variables      <br />
    /// Set initial values of variables
    /// </summary>
    void Start()
    {
        //Get Components from Object
        rb = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();

        //Get Components from Scene
        manager = GameObject.Find("Managers/GameManager").GetComponent<GameManager>();
        spawnerManager = GameObject.Find("Managers/ObjectSpawner").GetComponent<SpawnerManager>();
        // set camera to camera with name BBallCamera
        mainCamera = GameObject.Find("BBallCamera").GetComponent<Camera>();
        SetVariables();
    }

    /// <summary>
    /// Assign initial values when item spawns
    /// </summary>
    private void SetVariables()
    {
        rb.useGravity = false;
        lr.enabled = false;
        forceMultiplier = 3;
        linePoints = new List<Vector3>();
        lineSegmentCount = 40;
        maxTrajectoryLength = lineSegmentCount / 4;
    }

    /// <summary>
    /// Save mouse position when clicking on gameobject (as Vector3) <br />
    /// Enable the line renderer and update with initial line position
    /// </summary>
    private void OnMouseDown()
    {
        mouseInitialPos = Input.mousePosition;
        lr.enabled = true; //Show line only while clicking

        //Gets a more accurate world space position to use in trajectory start point (diffence in z-position between camera and object)
        //ChatGPT used to fix z-position calculation relating to the main camera
        mouseInitialWorldPos = mainCamera.ScreenToWorldPoint(new Vector3(mouseInitialPos.x, mouseInitialPos.y, Mathf.Abs(mainCamera.transform.position.z - transform.position.z)));
    }

    /// <summary>
    /// Update line renderer positions during mouse drag
    /// </summary>
    private void OnMouseDrag()
    {
        //Calculate forces for trajectory outline (same calc as in Throw() on mouseUp)
        Vector3 forceInit = Input.mousePosition - mouseInitialPos;
        Vector3 forceV = new Vector3(forceInit.x, forceInit.y, forceInit.y) * forceMultiplier;
        UpdateTrajectory(forceV, rb, mouseInitialWorldPos);
        float angle = CalculateCameraAngle(forceV);
        
        //Disable the line showing if over aim angle threshold (top 180deg of gameobject)
        if (angle <= 90)
        {
            lr.enabled = false;
        }
        else
        {
            lr.enabled = true;
        }
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
            manager.DecreaseShotsRemaining();
            spawnerManager.spawning = false;
            
            Throw(mouseInitialPos - mouseReleasePos);
            rb.useGravity = true;
        }
    }

    /// <summary>
    /// Throws the object with the specified force vector if it hasn't been thrown already
    /// </summary>
    /// <param name="force">The force vector to apply for the throw</param>
    private void Throw(Vector3 force)
    {
        if (bottleThrown) { return; } //Only able to throw object once

        float angle = CalculateCameraAngle(force);

        // Check if the angle is within the bottom 180 degrees
        if (angle <= 90)
        {
            rb.AddForce(new Vector3(force.x, force.y, force.y) * forceMultiplier);
            bottleThrown = true;
        }
    }

    /// <summary>
    /// Calculate angle between camera forward direction and mouse drag direction
    /// </summary>
    /// <param name="force">Calculated force vector to apply for the throw</param>
    /// <returns></returns>
    private float CalculateCameraAngle(Vector3 force)
    {
        Vector3 camForward = mainCamera.transform.forward;
        Vector3 mouseDragDir = force.normalized;
        float angle = Vector3.Angle(camForward, mouseDragDir);
        return angle;
    }

    /// <summary>
    /// Draws a trajectory away from the object showing throw velocity      <br />
    /// Stops line if it collides with any scene gameobject
    /// </summary>
    /// <param name="forceVector">Current force vector if mouse was released (x,y,z)*forcemultiplier</param>
    /// <param name="rb">Rigidbody of the object</param>
    /// <param name="startPoint">Initial mouse down click location</param>
    private void UpdateTrajectory(Vector3 forceVector, Rigidbody rb, Vector3 startPoint)
    {
        Vector3 velocity = forceVector / rb.mass * Time.fixedDeltaTime;

        // Calculate the maximum flight duration based on a desired maximum length
        float maxFlightDuration = Mathf.Sqrt(2 * maxTrajectoryLength / -Physics.gravity.y);

        // Calculate the step time based on the maximum flight duration and desired number of trajectory points
        float stepTime = maxFlightDuration / lineSegmentCount;

        // Reset line each frame, keep initial start point as first point in list
        linePoints.Clear();
        linePoints.Add(startPoint);

        // Calculate trajectory points along the flight path of the object
        for (int i = 1; i < lineSegmentCount; i++)
        {
            float stepTimePassed = stepTime * i; // Calculate the time passed for each trajectory point

            //Calculate any object displacement 
            Vector3 movementVector = new Vector3(
                velocity.x * stepTimePassed,
                velocity.y * stepTimePassed - 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed, //Calculates gravity effect on the vertical motion
                velocity.z * stepTimePassed);

            //Calcualte the new trajectory point with calculated move vector
            Vector3 newLinePoint = -movementVector + startPoint;

            RaycastHit hit;

            // Perform a raycast from the previous trajectory point to the new trajectory point to check for collisions along the trajectory path
            if (Physics.Raycast(linePoints[i - 1], newLinePoint - linePoints[i - 1], out hit, (newLinePoint - linePoints[i - 1]).magnitude))
            {
                linePoints.Add(hit.point);
                break; // Exit the loop since the trajectory is interrupted by collision
            }
            linePoints.Add(newLinePoint); // If no collision is detected, add the new trajectory point
        }
        lr.positionCount = linePoints.Count;
        lr.SetPositions(linePoints.ToArray()); //Display line following all stored positions
    }
}