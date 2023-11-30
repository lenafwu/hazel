using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int health;
    public Rigidbody2D rb;
    public Image[] hearts;

    public PlayerAttack playerAttack;
    public Animator animator;
    // Start is called before the first frame update
    public GameoverScreen gameoverScreen;

    private bool isDead = false;
    void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
        UpdateHeartsUI();
    }

    void Update(){

    }

    public void TakeDamage(int damage) {


        health -= damage;
        health = Mathf.Max(health, 0); 
        UpdateHeartsUI(); 
        playerAttack.FinishAttack();




        if (health <= 0) {
            Debug.Log("Player died!");
            Die();
            return;
        }


        animator.SetBool("isHit", true);

        StartCoroutine(RecoverFromHit());

        

        

    }

    IEnumerator RecoverFromHit()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(2.0f);
        animator.SetBool("isHit", false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Deadzone")){
            Debug.Log("Deadzone");
            Die();
        }
    }

    private void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = health > i * 2;
        }
    }
    public void Die(){

        animator.SetBool("isDead", true);
        animator.SetBool("isHit", false);

      //  gameObject.SetActive(false);
      //  gameoverScreen.Setup();
    }

    public void GameOver(){
        gameObject.SetActive(false);
        gameoverScreen.Setup();
    }
}
