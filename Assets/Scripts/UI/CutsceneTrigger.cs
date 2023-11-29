using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    public GameObject jelly;
    public Animator camAnim;
    public DialogueTrigger dialogueTrigger;
    private static bool isPlayed = false;
    void Start()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && !isPlayed)
        {
            camAnim.SetBool("cutscene1", true);
            print("Cutscene started");
            jelly.GetComponent<EnemyBehavior>().isHit = true;
            dialogueTrigger.StartDialogue();
            isPlayed = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            StopCutscene();
        }
    }

    void StopCutscene()
    {
        camAnim.SetBool("cutscene1", false);
        print("Cutscene ended");
    }
}
