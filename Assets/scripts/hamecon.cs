using UnityEngine;

public class Bait : MonoBehaviour
{
    [SerializeField] private float gravity = -1.5f;
    [SerializeField] private fildepeche rope; // drag your rope object here
    private Vector2 velocity;

    void Update()
    {
        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        transform.position += (Vector3)(velocity * Time.deltaTime);

        // Stop falling if rope is fully stretched
        if (rope != null)
        {
            Vector2 ropeEnd = rope.GetSecondToLastSegmentPos();
            float distance = Vector2.Distance(ropeEnd, (Vector2)transform.position);

            if (distance > rope.ropeSegLen)
            {
                velocity.y = 0f; // kill vertical velocity when taut
                transform.position = (Vector3)ropeEnd; // snap to rope end
            }
        }
    }
}