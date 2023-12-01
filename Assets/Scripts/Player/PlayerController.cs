using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float knockBackForce = 3f;
    public float knockBackCounter;
    public float knockBackTotalTime = 0.2f;

    public bool isKnockedFromRight;

    public GameObject healthBar;

    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 100f;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private LayerMask whatIsGround;

    private Rigidbody2D rb;
    private Animator anim;
    public bool isGrounded = false;

    private PlayerAttack playerAttack;

    private bool controlsEnabled = true;


    public AudioClip walkSound;
    public AudioClip jumpSound;
    public AudioClip hitSound;
    public AudioClip attackSound;
    private AudioSource audioSource;
    private float horizontalInput;
    private bool doubleJump;

    // void Awake()
    // {
    //     DontDestroyOnLoad(gameObject); 
    // }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update(){
        if (!controlsEnabled) return;


        horizontalInput = Input.GetAxis("Horizontal");

        if(GroundCheck() && !Input.GetButton("Jump")){
            doubleJump = false;
        }

        // this somehow works, don't touch
        if(Input.GetButtonDown("Jump")){
            if(GroundCheck() || doubleJump){
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);

                doubleJump = !doubleJump;
            }
        }

    }


    void FixedUpdate() {

        if (!controlsEnabled) return;

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


       
       isGrounded = GroundCheck();

       // Jump
       if(isGrounded && Input.GetAxis("Jump") > 0 ) {
            anim.SetTrigger("jumped");
            anim.SetBool("isGround", isGrounded);
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

    public void DisableControl()
    {
        controlsEnabled = false;
        rb.velocity = Vector2.zero;
        anim.SetFloat("xVelocity", 0);
        anim.SetFloat("yVelocity", 0);
        audioSource.enabled = false;    
        healthBar.SetActive(false);      
    }

    public void EnableControl()
    {
        controlsEnabled = true;
        audioSource.enabled = true;
        healthBar.SetActive(true);
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
