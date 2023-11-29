using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    private int collectedItems = 0;
    private AudioSource audioSource;
    public AudioClip itemSound;

    [SerializeField] private TMP_Text itemText;

    void Start(){
        itemText.text = "Candy: " + collectedItems + " / 20";
        audioSource = GetComponent<AudioSource>();
    }
   
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Collectable")){
            collectedItems++;
            Destroy(other.gameObject);
            itemText.text = "Candy: " + collectedItems + " / 20";
            audioSource.PlayOneShot(itemSound);
        }
    }

    public int GetCollectedItems(){
        return collectedItems;
    }
}
