using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Hitzone : MonoBehaviour
{
    public bool hitPlayer = false;
     private void OnTriggerEnter2D(Collider2D other) {  
        if(other.gameObject.CompareTag("Player")) {
            hitPlayer = true;
            Debug.Log("Player Hit");
        }
    }

    public bool PlayerIsHit(){
        return hitPlayer;
    }

    public void ResetHit(){
        hitPlayer = false;
    }
}
