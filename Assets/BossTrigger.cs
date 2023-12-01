using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BossTrigger : MonoBehaviour
{
    public PlayableDirector director;
    public GameObject bossDoor;

    public DialogueManager dialogueManager;

    private bool isPlayed = false;

    void Start(){
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && !isPlayed)
        {
            dialogueManager.director = director;
            director.Play();
            isPlayed = true;
            bossDoor.SetActive(true);
        }
    }
}
