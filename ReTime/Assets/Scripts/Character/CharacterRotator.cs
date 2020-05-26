using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotator : MonoBehaviour
{
    [Tooltip("Controls right click and drag rotation speed")][SerializeField] float rotateSpeed = 5f;
    float lastMouseXPos, mouseXPos;
    bool isRotating, startedRotating = false;

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
        if(Input.GetButton("Fire2"))
        {
            isRotating = true;
            if(Input.GetButtonDown("Fire2"))
            {
                startedRotating = true;
            }
        }
        else
        {
            isRotating = false;
        }
    }

    private void Rotate()
    {
        if (startedRotating)
        {
            lastMouseXPos = Input.mousePosition.x;
            startedRotating = false;
        } 
        if (isRotating)
        {
            mouseXPos = Input.mousePosition.x;
            float mouseMoveThisFrame = mouseXPos - lastMouseXPos;
            transform.Rotate(transform.up, mouseMoveThisFrame * rotateSpeed * Time.fixedDeltaTime);

            lastMouseXPos = mouseXPos;
        }
    }
}
