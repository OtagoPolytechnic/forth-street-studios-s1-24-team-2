using UnityEngine;

public class DragAndThrow : MonoBehaviour
{
    public LineRenderer trajectoryLine;
    public float maxPower = 10f;
    public float powerMultiplier = 1f;
    public float heightMultiplier = 0.5f; // Adjust this to control the height of the throw

    private Rigidbody rb;
    private Camera mainCamera;
    private bool isDragging = false;
    private Vector3 dragStartPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        trajectoryLine.positionCount = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    isDragging = true;
                    dragStartPosition = transform.position;
                }
            }
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            DisplayTrajectory();
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            ThrowObject();
            trajectoryLine.positionCount = 0;
        }
    }

    void DisplayTrajectory()
    {
        Vector3 start = transform.position;
        Vector3 direction = (GetMouseWorldPosition() - dragStartPosition).normalized;
        trajectoryLine.positionCount = 0;
        trajectoryLine.positionCount = 100;
        for (int i = 0; i < 100; i++)
        {
            float t = i / 100f;
            Vector3 position = start + direction * t * maxPower * powerMultiplier +
                Physics.gravity * t * t * 0.5f;
            trajectoryLine.SetPosition(i, position);
        }
    }

    void ThrowObject()
    {
        Vector3 direction = (dragStartPosition - GetMouseWorldPosition()).normalized;
        Vector3 force = direction * maxPower * powerMultiplier;
        force.y += Mathf.Abs(force.y) * heightMultiplier; // Add upward component
        rb.AddForce(force, ForceMode.Impulse);
    }

    Vector3 GetMouseWorldPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            return ray.GetPoint(distance);
        }
        return Vector3.zero;
    }
}
