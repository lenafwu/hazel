using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----- Audio Source -----")]
    public AudioSource musicSource;

    [Header("----- Audio Clip -----")]
    public AudioClip background;

    private void Awake() {
       DontDestroyOnLoad(transform.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        musicSource.volume = 0.2f;
        musicSource.clip = background;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
