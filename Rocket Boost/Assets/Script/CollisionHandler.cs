using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the collision of the rocket ship
/// </summary>
public class CollisionHandler : MonoBehaviour
{
    // The delay in seconds until reloading or loading next scene
    [SerializeField] float transitionDelay = 2f;

    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource = null;

    bool isControllable = true;
    bool isCollidable = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    private void Update()
    {
        RespondToDebugKeys();
    }

    /// <summary>
    /// When the l key is pressed it loads the next level
    /// </summary>
    private void RespondToDebugKeys()
    {
        if (Keyboard.current.lKey.isPressed == true)
        {
            LoadNextLevel();
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame == true)
        {
            isCollidable = !isCollidable;
        }
    }

    /// <summary>
    /// Looks at the tags on the objects collidied with the ship 
    /// </summary>
    /// <param name="other"> the object the ship is colliding with</param>
    private void OnCollisionEnter(Collision other) 
    {
        if (!isControllable || !isCollidable){return;}

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
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", transitionDelay);    
    }

    /// <summary>
    /// Starts the sequence of events required when the player completes a level
    /// </summary>
    private void StartNextLevelSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        successParticles.Play();
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
