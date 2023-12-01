using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip bossBGM;
    public AudioClip endingBGM;

    void Start(){
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayBossBGM(){
        audioSource.clip = bossBGM;
        audioSource.Play();
    }

    public void PlayEndingBGM(){
        audioSource.clip = endingBGM;
        audioSource.Play();
    }
}
