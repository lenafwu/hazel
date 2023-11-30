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

    public Animator animator;
    // Start is called before the first frame update
    public GameoverScreen gameoverScreen;
    void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
        UpdateHeartsUI();
    }

    public void TakeDamage(int damage) {
        health -= damage;

        health = Mathf.Max(health, 0); 
        animator.SetTrigger("hit");
        UpdateHeartsUI(); 

        if(health <= 0) {
            animator.SetTrigger("isDead");
        }
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
        gameObject.SetActive(false);
        gameoverScreen.Setup();
    }
}
