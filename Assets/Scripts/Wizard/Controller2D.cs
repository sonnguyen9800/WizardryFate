using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class Controller2D : MonoBehaviour
{


    // Setup the script
    BoxCollider2D boxCollider2D;
    private const float skinWidth = 0.015f;
    private RaycastOrigin raycastOrigin;


    [SerializeField][Range(4,16)] private int horizontalRayCount = 4;
    [SerializeField][Range(4,16)] private int verticalRayCount = 4;

    float horitonalSpaceRay;
    float verticalSpaceRay;
    [SerializeField] private LayerMask collisionMask;

    private void Awake() {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        CalculateRaySpaceing();

    }

    private void Update() {
    //  UpdateRaycastOrigin();
    //  CalculateRaySpaceing();
        
        // for (int i = 0; i < verticalRayCount; i ++) {
        //         Debug.DrawRay(raycastOrigin.bottomLeft + Vector2.right * verticalSpaceRay * i, Vector2.up * -2,Color.red);
        // }

    }


    // Move the component; translate the velocity
    public void Move(Vector2 velocity){
        UpdateRaycastOrigin(); // Update raycast origin
        VerticalCollision(ref velocity);
        transform.Translate(velocity);
    }

    void VerticalCollision(ref Vector2 velocity){
        float directionY = Mathf.Sign(velocity.y); // Get the direction of y-component
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;

        for (int i = 0; i < verticalRayCount; i ++){
            Vector2 rayOrigin = (directionY == -1)? raycastOrigin.bottomLeft:raycastOrigin.topLeft;
			rayOrigin += Vector2.right * (verticalSpaceRay * i + velocity.x);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

			Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength,Color.red);

			if (hit) {
                Debug.Log("COLLIDE");
				velocity.y = (hit.distance - skinWidth) * directionY;
				rayLength = hit.distance;
			}
        }

    }


    void UpdateRaycastOrigin(){
        Bounds bounds = boxCollider2D.bounds;
        bounds.Expand(skinWidth * (-2));

        raycastOrigin.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigin.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigin.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigin.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    public void CalculateRaySpaceing(){
        Bounds bounds = boxCollider2D.bounds;

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horitonalSpaceRay = bounds.size.y / (horizontalRayCount - 1);
        verticalSpaceRay = bounds.size.x / (verticalRayCount - 1);

    }


    struct RaycastOrigin {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }





    // Update is called once per frame

}
