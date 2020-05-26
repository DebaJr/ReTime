using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardGravityController : MonoBehaviour
{
    [Tooltip("Distance to shard stop falling and start floating")][SerializeField] float distanceToGround;
    [Tooltip("Same layer used for player ground collision detection")][SerializeField] LayerMask groundLayer;
    Rigidbody reTimerRigidBody;
    private void Awake() {
        reTimerRigidBody = GetComponent<Rigidbody>();
    }

    bool grounded;
    private void Update()
    {
        CheckGroundToStopGravity();
        if (grounded)
        {
            reTimerRigidBody.useGravity = false;
            reTimerRigidBody.velocity = Vector3.zero;
            GetComponent<CapsuleCollider>().enabled = true;
            GetComponent<CapsuleCollider>().isTrigger = true;
            Destroy(this);
        }
    }

    private void CheckGroundToStopGravity()
    {
        Vector3 _position = transform.position + new Vector3 (0f, 0.25f, 0f);
        grounded = Physics.OverlapSphere(_position, distanceToGround, groundLayer).Length > 0;
    }

        // Draw landingIdentifierGizmos
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Draw a sphere where the OverlapSphere is (positioned where your GameObject is as well as a size) -- On scene view, not in game!
        Vector3 _position = transform.position + new Vector3 (0f, 0.25f, 0f);
        Gizmos.DrawWireSphere(_position, distanceToGround);
    }
}
