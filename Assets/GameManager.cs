using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; private set; }

   public int maxHealth = 10;
   public int health = 10;

   private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Debug.Log("destroying duplicate game manager");
            Destroy(gameObject);
            return; // add this or it's buggy as hell
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
            ResetScore();
        }
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetScore();
    }

    private void OnDestroy()
    {
        // Unsubscribe when the object is destroyed to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    public void ReduceHealth(int damage)
    {
        health -= damage;
        health = Mathf.Max(health, 0);
    }
    private void ResetScore()
    {
        // reset something if needed
        
    }

    public void ResetHealth(){
        health = maxHealth;
    }
}
