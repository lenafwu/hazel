using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 100f;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private LayerMask whatIsGround;
    private Rigidbody2D rb;
    private Animator anim;
    public bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate() {
       float horizontalInput = Input.GetAxis("Horizontal");
       isGrounded = GroundCheck();

       if(isGrounded && Input.GetAxis("Jump") > 0 ) {
            anim.SetTrigger("jumped");
            anim.SetBool("isGround", isGrounded);
            print("jumped");
            rb.AddForce(new Vector2(0f, jumpForce));
            print(rb.velocity.y);
            isGrounded = false;
       }
       anim.SetBool("isGround", isGrounded);
       rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
       
       // flip the character
       if(horizontalInput < 0) {
           transform.localScale = new Vector3(-1, 1, 1);
       } else if(horizontalInput > 0) {
           transform.localScale = new Vector3(1, 1, 1);
       }
       // Communicate with the Animator
    //    print("xVelocity: " + rb.velocity.x);
    //    print("yVelocity: " + rb.velocity.y);
       anim.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
       anim.SetFloat("yVelocity", rb.velocity.y);

    }

    private bool GroundCheck(){
        return Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, whatIsGround);
    }
}
