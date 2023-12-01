using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float speed = 1.0f;
    public float distance = 2.0f;

    private bool movingRight = true;
    public bool isHit = false; // Flag for being hit
    private float startingX;
    private Animator anim;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        startingX = transform.position.x;
        anim.SetBool("isWalk", true);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHit)
        {
            Walking();
        }
    }

    void Walking()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        if (transform.position.x >= startingX + distance && movingRight)
        {
            movingRight = false;
            Flip();
        }
        else if (transform.position.x <= startingX - distance && !movingRight)
        {
            movingRight = true;
            Flip();
        }
    }

    public void Hit()
    {
        isHit = true;
        anim.SetTrigger("doTouch");
        anim.SetBool("isWalk", false);
        audioSource.Play();
        // 1 second after being hit, continue walking
        StartCoroutine(RecoverFromHit());
    }
 
     IEnumerator RecoverFromHit()
    {
        yield return new WaitForSeconds(2.0f); // Wait for 2 seconds
        isHit = false;
        anim.SetBool("isWalk", true);
    }

    void Flip() // FIXME: doesnt work
    {
        Debug.Log("Flip");
        // Vector3 localScale = transform.localScale;
        // if (movingRight)
        // {
        //     // Set scale to positive value to face right
        //     localScale.x = Mathf.Abs(localScale.x);
        // }
        // else
        // {
        //     // Set scale to negative value to face left
        //     localScale.x = -Mathf.Abs(localScale.x);
        // }
        // transform.localScale = localScale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hitzone"))
        {
            Hit();
        }
    }
}
