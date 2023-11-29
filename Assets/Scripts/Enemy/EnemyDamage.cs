using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyDamage : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public int damage = 2;
    public PlayerHealth playerHealth;
    public PlayerController playerController;
    public bool isHit = false;
    public Animator anim;
    public GameObject healthText;
    
    void Start(){
        currentHealth = maxHealth;
        healthText.SetActive(false);
    }

    void Update(){
        if(currentHealth <= 0){
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")) {
            playerController.knockBackCounter = playerController.knockBackTotalTime;

            // if the player is on the left
            if (other.transform.position.x < transform.position.x)
            {
                playerController.isKnockedFromRight = true;
            }
            else
            {
                playerController.isKnockedFromRight = false;
            }
            playerHealth.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Hitzone")){
            HitByPlayer();
        }
    }

    private void HitByPlayer(){
        isHit = true;
        anim.SetTrigger("doTouch");
        currentHealth -= damage;   
        StartCoroutine(RecoverFromHit()); 
        healthText.SetActive(true);
        healthText.GetComponent<TMP_Text>().text = currentHealth.ToString();

    }

    private void Die(){
        Destroy(gameObject);
    }

    IEnumerator RecoverFromHit()
    {
        yield return new WaitForSeconds(2.0f); // Wait for 2 seconds
        isHit = false;
    }
}
