using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;

public class DialogueManager : MonoBehaviour
{
    public Image actorImage;
    public TMP_Text actorName;
    public TMP_Text messageText;
    public GameObject backgroundBox;

    public PlayableDirector director;
    public static bool isDialogueOpen = false;

    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;

    public void OpenDialogue(Message[] messages, Actor[] actors)
    {
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isDialogueOpen = true;
        backgroundBox.SetActive(true);
        DisplayMessage();

        // Pause timeline
        if (director != null){
            director.Pause();
         }
    }

    void DisplayMessage(){
        Message message = currentMessages[activeMessage];
        Actor actor = currentActors[message.actorId];

        messageText.text = message.message;
        actorName.text = actor.name;
        actorImage.sprite = actor.sprite;
    }

    public void NextMessage(){
        activeMessage++;
        if(activeMessage < currentMessages.Length){
            DisplayMessage();
        } else {
            // Dialogue is over
            isDialogueOpen = false;
            backgroundBox.SetActive(false);

            // Resume the Timeline when dialogue ends
            if (director != null)
            {
                director.Resume();
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(isDialogueOpen){
            backgroundBox.SetActive(true);
        } else {
            backgroundBox.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space") && isDialogueOpen){
            NextMessage();
        }
    }
}
