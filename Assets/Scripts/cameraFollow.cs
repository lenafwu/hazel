using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform target; // The player's transform to follow.
    public float smoothSpeed = 0.125f; // Smoothing factor for camera movement.
    public Vector3 offset = new Vector3(0f, 0f, -10f); // Offset the camera from the player.

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the desired camera position.
            Vector3 desiredPosition = target.position + offset;

            // Use Vector3.SmoothDamp to smoothly move the camera towards the desired position.
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Update the camera's position.
            transform.position = smoothedPosition;
        }
    }
}
