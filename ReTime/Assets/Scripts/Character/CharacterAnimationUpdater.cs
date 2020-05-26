using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationUpdater : MonoBehaviour
{
    Animator animator;
    Rigidbody reTimerRigidbody;
    float xMove, zMove;
    bool jumped = false;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        reTimerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        xMove = Input.GetAxis("Horizontal");
        zMove = Input.GetAxis("Vertical");
        jumped = Input.GetButton("Jump");
    }

    private void FixedUpdate()
    {
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        animator.SetFloat("horizontalMove", xMove);
        animator.SetFloat("verticalMove", zMove);
        if (jumped)
        {
            animator.SetTrigger("jumped");
            jumped = false;
        }
        bool _idling = new Vector2(xMove,zMove) == Vector2.zero;
        animator.SetBool("Idling", _idling);
    }
}
