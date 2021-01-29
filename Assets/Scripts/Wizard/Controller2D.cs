using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Controller2D : MonoBehaviour
{
    [SerializeField] [Range(4, 16)] private int horizontalRayCount = 4;
    [SerializeField] [Range(4, 16)] private int verticalRayCount = 4;
    [SerializeField] private LayerMask collisionMask;
    private BoxCollider2D boxCollider2D;
    private const float skinWidth = 0.015f;
    private RaycastOrigin raycastOrigin;
    private CollisionInfo collisionInfo = new CollisionInfo();
    public CollisionInfo CollisionInfo { get => collisionInfo; set => collisionInfo = value; }
    private float horitonalSpaceRay, verticalSpaceRay;
    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        CalculateRaySpacing();
    }
    public void Move(Vector2 velocity)
    {
        UpdateRaycastOrigin();

        CollisionInfo.Reset();

        if (velocity.x != 0)
        {
            HorizontalCollision(ref velocity);
        }
        if (velocity.y != 0)
        {
            VerticalCollision(ref velocity);
        }
        //Debug.Log(velocity);
        transform.Translate(velocity);
    }

    private void VerticalCollision(ref Vector2 velocity)
    {
        float directionY = Mathf.Sign(velocity.y); // Get the direction of y-component
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigin.bottomLeft : raycastOrigin.topLeft;
            rayOrigin += Vector2.right * (verticalSpaceRay * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            if (hit)
            {
                //Debug.Log("COLLIDE");
                velocity.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;


                CollisionInfo.above = directionY == 1;
                CollisionInfo.below = directionY == -1;
            }
        }

    }


    private void HorizontalCollision(ref Vector2 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigin.bottomLeft : raycastOrigin.bottomRight;
            rayOrigin += Vector2.up * (verticalSpaceRay * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

            if (hit)
            {
                velocity.x = (hit.distance - skinWidth) * directionX;
                rayLength = hit.distance;


                CollisionInfo.left = directionX == -1;
                CollisionInfo.right = directionX == 1;

            }
        }
    }
    private void UpdateRaycastOrigin()
    {
        Bounds bounds = boxCollider2D.bounds;
        bounds.Expand(skinWidth * (-2));

        raycastOrigin.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigin.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigin.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigin.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    private void CalculateRaySpacing()
    {
        Bounds bounds = boxCollider2D.bounds;

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horitonalSpaceRay = bounds.size.y / (horizontalRayCount - 1);
        verticalSpaceRay = bounds.size.x / (verticalRayCount - 1);

    }
    private struct RaycastOrigin
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }
}
public class CollisionInfo
{
    public bool above, below, left, right;
    public void Reset()
    {
        above = below = left = right = false;
    }
}
