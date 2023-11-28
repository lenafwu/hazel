using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float knockBackForce = 3f;
    public float knockBackCounter;
    public float knockBackTotalTime = 0.2f;

    public bool isKnockedFromRight;

    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 100f;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private LayerMask whatIsGround;

    private Rigidbody2D rb;
    private Animator anim;
    public bool isGrounded = false;

    private PlayerAttack playerAttack;

    private int maxJumps = 2;
    private int _jumpsLeft;


    public AudioClip walkSound;
    public AudioClip jumpSound;
    public AudioClip hitSound;
    public AudioClip attackSound;
    private AudioSource audioSource;

    void Awake()
    {
        DontDestroyOnLoad(gameObject); 
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate() {
       // Stop moving
       if(playerAttack.IsAttacking() || DialogueManager.isDialogueOpen) {
            rb.velocity = new Vector2(0f, 0f);
            return;
       }

       // Knockback
       if(knockBackCounter > 0){

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(hitSound);
            }

            knockBackCounter -= Time.deltaTime;

            if(isKnockedFromRight){
            rb.velocity = new Vector2(-knockBackForce, knockBackForce * 0.2f);
            }else{
            rb.velocity = new Vector2(knockBackForce, knockBackForce* 0.2f);
            }
            return;
       }


       float horizontalInput = Input.GetAxis("Horizontal");
       isGrounded = GroundCheck();

       // Jump
       if(isGrounded && Input.GetAxis("Jump") > 0 ) {
            anim.SetTrigger("jumped");
            anim.SetBool("isGround", isGrounded);
            rb.AddForce(new Vector2(0f, jumpForce));
            isGrounded = false;
            PlaySound(jumpSound);
       }

       anim.SetBool("isGround", isGrounded);

       // Walk
       rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

       // Play walking sound
       /* FIXME: still looping when not moving
        if (isGrounded && Mathf.Abs(horizontalInput) > 0.1f && !audioSource.isPlaying)
        {
            PlaySound(walkSound, true);
        }
        else if (isGrounded && Mathf.Abs(horizontalInput) < 0.1f)
        {
            audioSource.loop = false;
            audioSource.Stop();
        }*/
       
       // Flip the character
       if(horizontalInput < 0) {
           transform.localScale = new Vector3(-1, 1, 1);
       } else if(horizontalInput > 0) {
           transform.localScale = new Vector3(1, 1, 1);
       }

       // Communicate with the Animator
       anim.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
       anim.SetFloat("yVelocity", rb.velocity.y);

    }

    private bool GroundCheck(){
        return Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, whatIsGround);
    }

    private void PlaySound(AudioClip clip, bool loop = false) {
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.Play();
    }

    public void PlayAttackSound()
    {
        audioSource.PlayOneShot(attackSound);
    }
}
