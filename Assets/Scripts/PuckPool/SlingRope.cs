using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingRope : MonoBehaviour
{
    public Transform StartPoint;
    public Transform EndPoint;

    public LineRenderer lineRenderer;
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();
    private float ropeSegLen = -0.2f;
    private int segmentLength = 35;
    private float lineWidth = 0.1f;

   // private bool movetomouse = false;
   // private Vector3 mousePositionworld;
   // private int indexmousePos;

    // Use this for initialization
    void Start()
    {
        
        this.lineRenderer = this.GetComponent<LineRenderer>();
        Vector3 ropeStartPoint = StartPoint.position;

        for (int i = 0; i < segmentLength; i++)
        {
            this.ropeSegments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y -= ropeSegLen;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //PolygonCollider2D();
        GeneratemeshCollider();
        this.DrawRope();
        /*if(Input.GetMouseButtonDown(0))
        {
            this.movetomouse = true;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            this.movetomouse = false;
        }
        Vector3 screenMousepos = Input.mousePosition;
        float xstart = StartPoint.position.x;
        float xend = EndPoint.position.x;
        this.mousePositionworld = Camera.main.ScreenToWorldPoint(new Vector3(screenMousepos.x, screenMousepos.y, 10));
        float currX = this.mousePositionworld.x;

        float ratio = (currX - xstart) / (xend - xstart);
        if(ratio > 0)
        {
            this.indexmousePos = (int)(this.segmentLength * ratio);
        }*/

    }

    private void FixedUpdate()
    {
        this.Simulate();
    }

    private void Simulate()
    {
        // SIMULATION
        Vector2 forceGravity = new Vector2(0f, -1f);

        for (int i = 1; i < this.segmentLength; i++)
        {
            RopeSegment firstSegment = this.ropeSegments[i];
            Vector2 velocity = firstSegment.posNow - firstSegment.posOld;
            firstSegment.posOld = firstSegment.posNow;
            firstSegment.posNow += velocity;
            firstSegment.posNow += forceGravity * Time.fixedDeltaTime;
            this.ropeSegments[i] = firstSegment;
        }

        //CONSTRAINTS
        for (int i = 0; i < 50; i++)
        {
            this.ApplyConstraint();
        }
    }

    private void ApplyConstraint()
    {
        //Constrant to First Point 
        RopeSegment firstSegment = this.ropeSegments[0];
        firstSegment.posNow = this.StartPoint.position;
        this.ropeSegments[0] = firstSegment;


        //Constrant to Second Point 
        RopeSegment endSegment = this.ropeSegments[this.ropeSegments.Count - 1];
        endSegment.posNow = this.EndPoint.position;
        this.ropeSegments[this.ropeSegments.Count - 1] = endSegment;

        for (int i = 0; i < this.segmentLength - 1; i++)
        {
            RopeSegment firstSeg = this.ropeSegments[i];
            RopeSegment secondSeg = this.ropeSegments[i + 1];

            float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
            float error = Mathf.Abs(dist - this.ropeSegLen);
            Vector2 changeDir = Vector2.zero;

            if (dist > ropeSegLen)
            {
                changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            }
            else if (dist < ropeSegLen)
            {
                changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
            }

            Vector2 changeAmount = changeDir * error;
            if (i != 0)
            {
                firstSeg.posNow -= changeAmount * 0.5f;
                this.ropeSegments[i] = firstSeg;
                secondSeg.posNow += changeAmount * 0.5f;
                this.ropeSegments[i + 1] = secondSeg;
            }
            else
            {
                secondSeg.posNow += changeAmount;
                this.ropeSegments[i + 1] = secondSeg;
            }
           /* if(this.movetomouse && indexmousePos > 0 && indexmousePos < this.segmentLength -1 && i == indexmousePos)
            {
                RopeSegment segment = this.ropeSegments[i];
                RopeSegment segment2 = this.ropeSegments[i];
                segment.posNow = new Vector2(this.mousePositionworld.x, this.mousePositionworld.y);
                segment2.posNow = new Vector2(this.mousePositionworld.x, this.mousePositionworld.y);
                this.ropeSegments[i] = segment;
                this.ropeSegments[i + 1] = segment2;

            }*/
        }
    }

    private void DrawRope()
    {
        float lineWidth = this.lineWidth;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

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
    // MeshCollider 
    public void GeneratemeshCollider()
    {
        MeshCollider collider = GetComponent<MeshCollider>();

        if(collider == null)
        {
            collider = gameObject.AddComponent<MeshCollider>();   
        }
       collider.convex = true;
        Mesh mesh = new Mesh();
        lineRenderer.BakeMesh(mesh,true);
        //collider.sharedMesh = mesh;
    }
    //PolygonCollider2D
    public void PolygonCollider2D()
    {
        PolygonCollider2D collider = GetComponent<PolygonCollider2D>();
        if(collider == null)
        {
            collider = gameObject.AddComponent<PolygonCollider2D>();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Strikers")
        {
            Debug.Log("trigger");
        }
    }
}
