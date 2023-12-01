using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CassTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public DialogueTrigger dialogueTrigger;
    public Animator camAnim;
    private static bool isPlayed = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && !isPlayed)
        {
            Debug.Log("Cutscene2 started");
            camAnim.SetBool("cutscene2", true);
            dialogueTrigger.StartDialogue();
            isPlayed = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            camAnim.SetBool("cutscene2", false);
        }
    }
}
