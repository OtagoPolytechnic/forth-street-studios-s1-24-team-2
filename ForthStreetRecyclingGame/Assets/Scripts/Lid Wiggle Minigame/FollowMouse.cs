using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private float smoothVelocity = 1;
    [SerializeField] private Camera cam;
    [SerializeField] private int buffer = 20;
    private Vector3 mousePos;
    private float previousX, currentX; // Variables to store the x positions
    private float direction = 0f;

    void Start()
    {
        cam = (LidWiggleManager.instance != null) ? LidWiggleManager.instance.minigameCamera : Camera.main;
        mousePos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (LidWiggleManager.instance != null && !LidWiggleManager.instance.gameStarted) { return; }

        if (IsMouseOnScreen())
        {
            mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -cam.transform.position.z));
        }

        Vector3 targetPos = new Vector3(mousePos.x, mousePos.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothVelocity * Time.deltaTime);

        // Store the difference between current and previous x positions
        currentX = transform.position.x; // Update currentX with the GameObject's x position

        // Check if x direction has changed
        if (Mathf.Sign(currentX - previousX) != direction)
        {
            direction = Mathf.Sign(currentX - previousX);
            if (LidWiggleManager.instance.IsFillMax == false)
            {
                SFXManager.Instance.Play("Woosh");
            }
        }

        previousX = currentX; // Upda
    }

    private bool IsMouseOnScreen()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        return mouseScreenPos.x >= buffer && mouseScreenPos.x <= Screen.width - buffer && mouseScreenPos.y >= buffer && mouseScreenPos.y <= Screen.height - buffer;
    }

    public void Reset()
    {
        mousePos = transform.position;
    }
}