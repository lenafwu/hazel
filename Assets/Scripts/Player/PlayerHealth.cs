using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public Rigidbody2D rb;
    public Image[] hearts;
    public PlayerAttack playerAttack;
    public Animator animator;
    // Start is called before the first frame update
    public GameoverScreen gameoverScreen;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
        UpdateHeartsUI();
    }

    public void TakeDamage(int damage) {
        GameManager.Instance.ReduceHealth(damage);
        UpdateHeartsUI(); 
        playerAttack.FinishAttack();

        if(GameManager.Instance.health <= 0){
            Die();
        }else{
            animator.SetBool("isHit", true);
            StartCoroutine(RecoverFromHit());
        }      
    }

    IEnumerator RecoverFromHit()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(1.0f);
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
            hearts[i].enabled = GameManager.Instance.health > i * 2;
        }
    }
    public void Die(){

        animator.SetBool("isDead", true);
        animator.SetBool("isHit", false);
        GameManager.Instance.ResetHealth();


      //  gameObject.SetActive(false);
      //  gameoverScreen.Setup();
    }

    public void GameOver(){
        gameObject.SetActive(false);
        gameoverScreen.Setup();
    }
}
