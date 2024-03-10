//Tutorial used for final help: https://www.youtube.com/watch?v=99yIg-A5eCw
// Modified the direction of ball launch (away instead of towards camera)

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