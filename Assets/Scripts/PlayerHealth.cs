using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int health;
    public Image[] hearts;
    // Start is called before the first frame update
    public GameoverScreen gameoverScreen;
    void Start()
    {
        health = maxHealth;
        UpdateHeartsUI();
    }

    public void TakeDamage(int damage) {
        health -= damage;

        health = Mathf.Max(health, 0); 
        UpdateHeartsUI(); 

        if(health <= 0) {
            Die();
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
    private void Die(){
        print("I died!");
        gameObject.SetActive(false);
        gameoverScreen.Setup();
    }
}
