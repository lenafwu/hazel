using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameoverScreen : MonoBehaviour
{
    public void Setup(){
        gameObject.SetActive(true);
    }

    public void RestartButton(){
        // reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
