using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("Player move speed")][SerializeField] float speed = 10f;
    float horizontalInput, verticalInput;
    Rigidbody reTimerRigidBody;
    private void Awake()
    {
        reTimerRigidBody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        CaptureInput();
    }

    
    private void FixedUpdate() 
    {
        Translate();
    }

    private void CaptureInput()
    {
        //Movement Input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void Translate()
    {
        Vector3 _horizontalMove = transform.right * horizontalInput;
        Vector3 _verticalMove = transform.forward * verticalInput;
        Vector3 _move = (_horizontalMove + _verticalMove).normalized * speed * Time.deltaTime;
        if(_move != Vector3.zero)
        {
            reTimerRigidBody.MovePosition(transform.position + _move);
        }
    }
}
