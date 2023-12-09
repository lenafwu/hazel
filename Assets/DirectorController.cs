using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DirectorController : MonoBehaviour
{
    private PlayableDirector director;
    public static bool hasPlayedCutscene = false;

    void Awake()
    {
        director = GetComponent<PlayableDirector>();

        if (!hasPlayedCutscene)
        {
            director.Play();
            hasPlayedCutscene = true;
        }
    }
}
