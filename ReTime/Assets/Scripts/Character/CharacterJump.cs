using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : MonoBehaviour
{
    [Header("Jumping and Gravity")]
    [SerializeField] float jumpForce = 20f;
    [Tooltip("Value to be applied as gravity while jumping, to achieve a more gaming jump arc")][SerializeField] float specialGravity = -75f;
    [Tooltip("Layer for ground checks")][SerializeField] LayerMask groundLayer;
    [Tooltip("Size for the ground checker box")][SerializeField] Vector3 groundCheckerSize;
    bool pressedJump = false;
    bool isOnGround = true;
    float yVelocity;
    Rigidbody reTimerRigidBody;

    void Awake()
    {
        reTimerRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CaptureJumpInput();
        PreventGroundIntrusion();
    }

    private void FixedUpdate() 
    {
        Jump();
        GravityController(yVelocity);
    }

    private void CaptureJumpInput()
    {
        //Jump Input
        pressedJump = Input.GetButtonDown("Jump");
        Vector3 groundCheckerCenterPoint = new Vector3 (transform.position.x, transform.position.y - (groundCheckerSize.y * 0.5f), transform.position.z);
        isOnGround = Physics.OverlapBox(groundCheckerCenterPoint, groundCheckerSize, Quaternion.identity, groundLayer).Length > 0;
        
        //captures last frame velocity to gravity calculations
        yVelocity = reTimerRigidBody.velocity.y;
    }

    private void Jump()
    {
        if(pressedJump && isOnGround)
        {
            reTimerRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            pressedJump = false;
        }
    }

    // Draw landingIdentifierGizmos
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size) -- On scene view, not in game!
        Vector3 groundCheckerCenterPoint = new Vector3 (transform.position.x, transform.position.y - (groundCheckerSize.y * 0.5f), transform.position.z);
        Gizmos.DrawWireCube(groundCheckerCenterPoint, groundCheckerSize);
    }

    void GravityController (float _yVelocity)
    {
        if(_yVelocity < -0.01f && !isOnGround)
        {
            reTimerRigidBody.useGravity = false;
            reTimerRigidBody.AddRelativeForce(specialGravity * Vector3.up, ForceMode.Acceleration);
        }
        else if (_yVelocity >= 0f && !isOnGround)
        {
            reTimerRigidBody.useGravity = false;
            reTimerRigidBody.AddRelativeForce(specialGravity * Vector3.up * 0.5f, ForceMode.Acceleration);
        }
        else
        {
            // prevents floating on edge of platforms
            reTimerRigidBody.useGravity = true;
        }
    }

    void PreventGroundIntrusion()
    {
        if(transform.position.y < -0.001f)
            {
                transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            }
    }
}
