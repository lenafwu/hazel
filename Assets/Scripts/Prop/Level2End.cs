using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Level2End : MonoBehaviour
{
    public ItemCollector itemCollector;
    public TMP_Text signText;
    public GameObject panel;
    public int currentItems = 0;

    public int requiredItems = 20;

    public bool playerIsNear;

    public AudioSource audioSource;
    public AudioClip signSound;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        signText.text = "Collect " + requiredItems + " candies and come back here!";
        panel = GameObject.Find("Panel");
        panel.SetActive(false);
        signText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        currentItems = itemCollector.GetCollectedItems();
        if (currentItems >= requiredItems)
        {
            signText.text = "Press E to go to the next level!";
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        if(playerIsNear){
            panel.SetActive(true);
            signText.gameObject.SetActive(true);
            
            
        }else{
            panel.SetActive(false);
            signText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            playerIsNear = true;
            // play one time

              //  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
              if(!audioSource.isPlaying){
                audioSource.PlayOneShot(signSound);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            playerIsNear = false;
        }
    }
}
