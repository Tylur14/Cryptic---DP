using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSimple2dCharacterMotor : MonoBehaviour
{
    /// <summary>
    /// BOUNTY: Find fix for falling through floor without delay work around
    /// </summary>
    
    [Header("Movement")]
    public Vector2 velocity;
    [SerializeField] private Vector2 horizontalVelocityBounds;
    private Vector2 horizontalVelocityRealBounds;
    [SerializeField] private Vector2 verticalVelocityBounds;
    private Vector2 verticalVelocityRealBounds;
    
    [SerializeField] private bool canWallJump;
    [Space(10)]
    [Header("Forces")]
    [SerializeField] private float moveForce = 150f;
    [SerializeField] private float jumpButtonTime = 0.2f;
    [SerializeField] private float jumpForce = 90f;
    [SerializeField] private float slopeSlideForce = 90f;
    [SerializeField, Range(0f, 1f)] private float slopeLimit = 0.45f;
    [SerializeField] private float drag = 20f;
    [SerializeField] private float jumpGravity = 24f;
    [SerializeField] private float fallGravity = 48f;
    [SerializeField] private float moveTolerance;
    [SerializeField] private LayerMask groundLayer;
    public bool isGrounded, hitLeftWall, hitRightWall, hitCeiling, isJumping, isMoving;
    [Space(10)]
    [Header("Corners")]
    [SerializeField] public Transform topLeft;
    [SerializeField] public Transform bottomLeft;
    [SerializeField] public Transform topRight;
    [SerializeField] public Transform bottomRight;
    [Space(10)]
    [Header("Vertical Checks")]
    [SerializeField] private float groundCheckRayLength = 0.05f;
    [SerializeField] private int groundRayCount = 6;
    [Space(10)]
    [Header("Horizontal Checks")]
    [SerializeField] private float wallCheckRayLength = 0.07f;
    [SerializeField] private int wallRayCount = 8;

    private Quaternion hitObjectRotation;
    public Vector3 hitObjectPosition;
    private float jumpTimer;
    private bool isLocked = true;

    private void Start()
    {
        Invoke("DelayUnlockVelocity",0.35f);
    }

    private void DelayUnlockVelocity()
    {
        isLocked = false;
    }

    private void Update()
    {
        if (isLocked) return;
        CheckCollisions();
    }

    private void FixedUpdate()
    {
        if (isLocked) return;
        ApplyVelocity();
    }

    public void AddMoveForce(Vector2 input)
    {
        velocity.x += input.x * moveForce * Time.deltaTime;
        //velocity.y += input.y * moveForce * Time.deltaTime;
    }
    
    protected virtual void CheckCollisions()
    {
        CheckVerticalCollisions();
        CheckHorizontalCollisions();
    }

    private void CheckVerticalCollisions()
    {
        // Ground
        var down = Vector2.down;
        var groundOrigin = (Vector2)bottomLeft.position;
        var groundDestination = (Vector2)bottomRight.position;
        groundDestination.y = groundOrigin.y;
        
        isGrounded = CheckCollisionRange(groundOrigin, groundDestination, down, groundRayCount, groundCheckRayLength);
        
        if(isGrounded)
        {
            var correctionOrigin = bottomLeft.position;
            var correctionDestination = bottomRight.position;
            correctionOrigin.y += 0.1f;
            correctionDestination.y = correctionOrigin.y;
            var correctionRayLength = Vector3.Distance(topLeft.position,bottomLeft.position) + 0.1f;
            CheckCollisionRange(correctionOrigin, correctionDestination, down, groundRayCount, correctionRayLength);
            if (!(transform.position.y > hitObjectPosition.y))
            {
                var pos = transform.position;
                pos.y = hitObjectPosition.y;
                transform.position = pos;
            }
            
            verticalVelocityRealBounds.x = 0;
            if (CheckCollisionRange(groundOrigin, groundDestination, down, groundRayCount,
                2f))
            {
                float angle = hitObjectRotation.z;
                int dir = angle < 0 ? -1 : 1;
                angle = Mathf.Abs(angle);
                // print($"Angle: {angle}");
                if (angle > slopeLimit)
                {
                    // print("Sliding");
                    velocity.x -= slopeSlideForce * dir * Time.deltaTime;
                    velocity.y -= slopeSlideForce/2 * dir * Time.deltaTime;
                }
            }
        }
        else verticalVelocityRealBounds.x = verticalVelocityBounds.x;
        
        
        
        // ceiling
        var up = Vector2.up;
        var ceilingOrigin = (Vector2)topLeft.position;
        var ceilingDestination = (Vector2)topRight.position;
        ceilingDestination.y = ceilingOrigin.y;
        
        hitCeiling = CheckCollisionRange(ceilingOrigin, ceilingDestination, up, groundRayCount, groundCheckRayLength);
        if (hitCeiling)
        {
            verticalVelocityRealBounds.y = 0;
        }
        else verticalVelocityRealBounds.y = verticalVelocityBounds.y;
    }

    private void CheckHorizontalCollisions()
    {
        // Left Wall
        var left = Vector2.left;
        var leftOrigin = (Vector2)topLeft.position;
        var leftDestination = (Vector2)bottomLeft.position;
        hitLeftWall = CheckCollisionRange(leftOrigin, leftDestination, left, wallRayCount, wallCheckRayLength);
        if(hitLeftWall)
        {
            horizontalVelocityRealBounds.x = 0;
        }
        else horizontalVelocityRealBounds.x = horizontalVelocityBounds.x;
        
        // Right Wall
        var right = Vector2.right;
        var rightOrigin = (Vector2)topRight.position;
        var rightDestination = (Vector2)bottomRight.position;
        hitRightWall = CheckCollisionRange(rightOrigin, rightDestination, right, wallRayCount, wallCheckRayLength);
        if(hitRightWall)
        {
            horizontalVelocityRealBounds.y = 0;
        }
        else horizontalVelocityRealBounds.y = horizontalVelocityBounds.y;
    }

    private bool CheckCollisionRange(Vector2 origin, Vector2 destination, Vector2 direction, int rayCount, float rayLength)
    {
        bool hitSomething = false;
        var moveOffsetX = ((origin.x - destination.x)/rayCount)*-1;
        var moveOffsetY = ((origin.y - destination.y)/rayCount)*-1;
        for (int i = 0; i < rayCount - 1; i++)
        {
            origin.x += moveOffsetX;
            origin.y += moveOffsetY;
            var hit = Physics2D.Raycast(origin, direction, rayLength, groundLayer);
            if (hit.collider != null)
            {
                hitObjectRotation = hit.transform.rotation;
                hitObjectPosition = hit.point;
                Debug.DrawRay(origin,direction * rayLength, Color.red, .01f);
                hitSomething = true;
            }
            else Debug.DrawRay(origin,direction * rayLength, Color.white, .01f);
        }

        return hitSomething;

    }

    public void Jump()
    {
        if (!isJumping && isGrounded || canWallJump && (hitLeftWall || hitRightWall))
            isJumping = true;
        if (jumpTimer < jumpButtonTime && isJumping)
        {
            velocity.y += jumpForce * Time.deltaTime;
            jumpTimer += Time.deltaTime;
        }
        else if (jumpTimer > jumpButtonTime)
            StopJump();
    }

    public void StopJump()
    {
        isJumping = false;
        jumpTimer = 0;
    }
    
    protected virtual void ApplyVelocity()
    {
        float gravity = isJumping ? jumpGravity : fallGravity;
        velocity.y -= gravity * Time.deltaTime;
        velocity.x = Mathf.Lerp(velocity.x, 0, drag * Time.deltaTime);
        
        velocity.x = Mathf.Clamp(velocity.x, horizontalVelocityRealBounds.x, horizontalVelocityRealBounds.y);
        velocity.y = Mathf.Clamp(velocity.y, verticalVelocityRealBounds.x, verticalVelocityRealBounds.y);

        isMoving = Mathf.Abs(velocity.x) > moveTolerance;
        
        var pos = (Vector2)transform.position;
        pos += velocity;
        transform.position = Vector3.Lerp(transform.position,pos,Time.deltaTime);
    }
}
