using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotator : MonoBehaviour
{
    [Tooltip("Controls right click and drag rotation speed")][SerializeField] float rotateSpeed = 5f;
    [Tooltip("Minimun difference between frames of the mouse or stick position to rotate the camera")][SerializeField] float rotateSensibility = 0.05f;
    float xMove, lastMovePos;

    float angle = 0f;

    void Update()
    {
        CaptureRotationInput();
    }

    private void FixedUpdate()
    {
        Rotate();
    }

    private void CaptureRotationInput()
    {
        if (Input.GetAxis("Mouse X") != lastMovePos)
        {
            xMove = Input.GetAxis("Mouse X");
            lastMovePos = xMove;
        }
        else if (Input.GetAxis("RightStick") != lastMovePos)
        {
            xMove = Input.GetAxis("RightStick");
            lastMovePos = xMove;
        }
        else
        {
            xMove = 0f;
        }
    }

    private void Rotate()
    {
        angle = xMove * rotateSpeed * Time.fixedDeltaTime;
        transform.Rotate(Vector3.up, angle);
    }
}
