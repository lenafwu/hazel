using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class Boss_Attack : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public GameObject hitzone;
    public PlayerHealth playerHealth;
    public PlayerController playerController;
    public GameObject player;

    public Boss_Hitzone boss_hitzone;
    
    public Image healthBar;
    public Animator anim;

    public int damage = 4;
    public AudioSource audioSource;
    public AudioClip attackSound;

    public PlayableDirector director;


    void Start(){
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        boss_hitzone = hitzone.GetComponent<Boss_Hitzone>();
        currentHealth = maxHealth;
    }
    void Update(){


        if(boss_hitzone.PlayerIsHit()){
            playerController.knockBackCounter = playerController.knockBackTotalTime;

            // if the player is on the left
            if (player.transform.position.x < transform.position.x)
            {
                playerController.isKnockedFromRight = true;
            }
            else
            {
                playerController.isKnockedFromRight = false;
            }
            playerHealth.TakeDamage(damage);
            boss_hitzone.ResetHit();
        }
    }

     private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Hitzone")){
            HitByPlayer();
        }
    }
    
    public void Attack(){
        hitzone.SetActive(true);
        audioSource.PlayOneShot(attackSound);
    }

    public void StopAttack(){
        Debug.Log("Cassandra Stopped Attacking");
        anim.ResetTrigger("Attack");
        hitzone.SetActive(false);
    }

    private void HitByPlayer(){
        audioSource.Stop();
        anim.SetBool("isHit", true);
        currentHealth -= 50;

        if(currentHealth <= 0){
            Die();
        }

        StopAttack();
        healthBar.fillAmount = (maxHealth - currentHealth) / maxHealth;
        StartCoroutine(RecoverFromHit());
    }

    IEnumerator RecoverFromHit()
    {

        yield return new WaitForSeconds(2.0f); 
        anim.SetBool("isHit", false);
        hitzone.SetActive(false);
        // Wait for 2 seconds
    }


    private void Die(){
        Debug.Log("Cassandra Died");
        anim.SetBool("isDead", true);
        director.Play();
    }
    
}
