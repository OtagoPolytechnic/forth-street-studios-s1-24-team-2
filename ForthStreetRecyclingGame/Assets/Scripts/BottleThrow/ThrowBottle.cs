using UnityEngine;

public class ThrowBottle : MonoBehaviour
{
    private Vector3 mouseInitialPos;
    private Vector3 mouseReleasePos;
    private Rigidbody rb;
    private bool bottleThrown;

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
        Shoot(mouseInitialPos - mouseReleasePos);
    }

    private float forceMultiplier = 3;
    void Shoot(Vector3 Force)
    {
        if (bottleThrown)
            return;

        rb.AddForce(new Vector3(Force.x, Force.y, Force.y) * forceMultiplier);
        bottleThrown = true;
    }
}