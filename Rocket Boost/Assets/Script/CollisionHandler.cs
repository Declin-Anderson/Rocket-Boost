using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the collision of the rocket ship
/// </summary>
public class CollisionHandler : MonoBehaviour
{
    // The delay in seconds until reloading or loading next scene
    [SerializeField] float transitionDelay = 2f;

    /// <summary>
    /// Looks at the tags on the objects collidied with the ship 
    /// </summary>
    /// <param name="other"> the object the ship is colliding with</param>
    private void OnCollisionEnter(Collision other) 
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Hit Friendly");
                break;
            case "Fuel":
                Debug.Log("Hit Fuel");
                break;
            case "Finish":
                StartNextLevelSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    /// <summary>
    /// Starts the sequence of events required when crashed
    /// </summary>
    private void StartCrashSequence()
    {
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", transitionDelay);    
    }

    /// <summary>
    /// Starts the sequence of events required when the player completes a level
    /// </summary>
    private void StartNextLevelSequence()
    {
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", transitionDelay);
    }

    /// <summary>
    /// Reloads the current scene
    /// </summary>
    private void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    /// <summary>
    /// Loads the next level when the rocket ship lands on the finish pad
    /// </summary>
    private void LoadNextLevel()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }
}
