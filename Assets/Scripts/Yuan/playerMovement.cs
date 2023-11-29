using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this to control the movement speed.
    public float jumpForce = 10f; // Adjust this to control the jump force.
    private bool isGrounded; // Check if the player is grounded.
    private Rigidbody2D rb;

    private SpriteRenderer spriteRenderer;
    private float originalScaleX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalScaleX = transform.localScale.x;
    }

    
    void Update()
    {
        // Get the horizontal input axis (A and D keys or Left and Right arrow keys).
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate the velocity for horizontal movement.
        Vector2 movement = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // Apply the velocity to the Rigidbody2D.
        rb.velocity = movement;

        // Check for jump input (spacebar).
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Apply an upward force to jump.
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }


        // Flip the sprite's X-axis scale based on the direction.
        if (horizontalInput < 0)
        {
            // Moving left, flip the sprite.
            transform.localScale = new Vector3(-originalScaleX, transform.localScale.y, transform.localScale.z);
        }
        else if (horizontalInput > 0)
        {
            // Moving right, restore the original scale.
            transform.localScale = new Vector3(originalScaleX, transform.localScale.y, transform.localScale.z);
        }
    }
}

