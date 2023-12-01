using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndingUI : MonoBehaviour
{
    public GameObject canvas;
    public void EnableUI(){
        canvas.SetActive(true);
    }
    public void GoToMainMenu(){
        SceneManager.LoadScene("main-menu");
    }

    public void QuitGame(){
        Application.Quit();
    }
}
