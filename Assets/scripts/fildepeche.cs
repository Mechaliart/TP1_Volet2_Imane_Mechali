using UnityEngine;
using System.Collections.Generic;

public class fildepeche : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();
    // private float ropeSegLen = 0.25f;
    private int segmentLength = 20; // permet de régler la longueur du fil de peche
    private float lineWidth = 0.05f;
    public float ropeSegLen = 0.25f; // Dans hamecon.cs se trouve cette variable, il faut la rendre publique pour y accéder depuis le script du hameçon

    [SerializeField] private int constraintIterations = 50; 
    [SerializeField] private Vector2 gravity = new Vector2(0f, -1.5f);

    [Header("Fishing Rod")]
    [SerializeField] private Transform rodTip;   // Assign the rod tip Transform
    [SerializeField] private Transform bait;     // Assign the bait Transform

    void Start()
    {
        this.lineRenderer = this.GetComponent<LineRenderer>();

        // Initialize rope from rod tip downward
        Vector3 ropeStartPoint = rodTip != null ? rodTip.position : transform.position;

        for (int i = 0; i < this.segmentLength; i++)
        {
            this.ropeSegments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y -= this.ropeSegLen;
        }
    }

    void Update()
    {
        this.Simulate();
        this.DrawRope();
    }

    private void Simulate()
    {
        // Verlet integration
        for (int i = 0; i < this.segmentLength; i++)
        {
            RopeSegment segment = this.ropeSegments[i];
            Vector2 velocity = segment.posNow - segment.posOld;
            segment.posOld = segment.posNow;
            segment.posNow += velocity;
            segment.posNow += this.gravity * Time.deltaTime;
            this.ropeSegments[i] = segment;
        }

        // Constraint solving
        for (int iteration = 0; iteration < constraintIterations; iteration++)
        {
            ApplyConstraints();
        }
    }

    private void ApplyConstraints()
    {
        // --- Pin the FIRST segment to the rod tip ---
        if (rodTip != null)
        {
            RopeSegment firstSegment = this.ropeSegments[0];
            firstSegment.posNow = rodTip.position;
            this.ropeSegments[0] = firstSegment;
        }

        // --- Pin the LAST segment to the bait ---
        if (bait != null)
        {
            RopeSegment lastSegment = this.ropeSegments[this.segmentLength - 1];
            lastSegment.posNow = bait.position;
            this.ropeSegments[this.segmentLength - 1] = lastSegment;
        }

        // --- Enforce distance constraints along the rope ---
        for (int i = 0; i < this.segmentLength - 1; i++)
        {
            RopeSegment segA = this.ropeSegments[i];
            RopeSegment segB = this.ropeSegments[i + 1];

            float dist = (segA.posNow - segB.posNow).magnitude;
            float error = dist - this.ropeSegLen;
            Vector2 changeDir = (segA.posNow - segB.posNow).normalized;
            Vector2 changeAmount = changeDir * error;

            // Both ends are pinned, so always split correction 50/50
            segA.posNow -= changeAmount * 0.5f;
            segB.posNow += changeAmount * 0.5f;

            this.ropeSegments[i] = segA;
            this.ropeSegments[i + 1] = segB;
        }
    }

    private void DrawRope()
    {
        lineRenderer.startWidth = this.lineWidth;
        lineRenderer.endWidth = this.lineWidth;

        Vector3[] ropePositions = new Vector3[this.segmentLength];
        for (int i = 0; i < this.segmentLength; i++)
        {
            ropePositions[i] = this.ropeSegments[i].posNow;
        }

        lineRenderer.positionCount = ropePositions.Length;
        lineRenderer.SetPositions(ropePositions);
    }

    public struct RopeSegment
    {
        public Vector2 posNow;
        public Vector2 posOld;

        public RopeSegment(Vector2 pos)
        {
            this.posNow = pos;
            this.posOld = pos;
        }
    }

public Vector2 GetSecondToLastSegmentPos()
{
    return ropeSegments[segmentLength - 2].posNow;
}
}