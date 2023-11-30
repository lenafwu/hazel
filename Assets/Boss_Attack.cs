using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Attack : MonoBehaviour
{
    public GameObject hitzone;
    public PlayerHealth playerHealth;
    public PlayerController playerController;
    public GameObject player;

    public Boss_Hitzone boss_hitzone;

    public int damage = 4;


    void Start(){
        boss_hitzone = hitzone.GetComponent<Boss_Hitzone>();
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
    
    public void Attack(){
        Debug.Log("Cassandra Attacked");
        hitzone.SetActive(true);
    }

    public void StopAttack(){
        Debug.Log("Cassandra Stopped Attacking");
        hitzone.SetActive(false);
    }

    
    
}
