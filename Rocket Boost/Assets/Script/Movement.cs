using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles the movement of the spaceship
/// </summary>
public class Movement : MonoBehaviour
{
    // The input that applies the thrust
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    Rigidbody rb;

    // Strength of the thrust that the spaceship applies
    [SerializeField]float thrustStrength = 0;
    [SerializeField]float rotationStrength = 0;

    AudioSource audioSource = null;

    /// <summary>
    /// When this object is enabled in the scene
    /// </summary>
    private void OnEnable() 
    {
        thrust.Enable();
        rotation.Enable();
    }
    
    /// <summary>
    /// Called when the object initialized
    /// </summary>
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Called during each tick of the game
    /// </summary>
    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    /// <summary>
    /// When the thrust button is pressed it applies a force up
    /// </summary>
    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            // If the audio isn't playing already play the audio
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
 
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
        }
        else
        {
            audioSource.Stop();
        }
    }

    /// <summary>
    /// When the rotation button is pressed it applies a rotation to the spaceship
    /// </summary>
    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if(rotationInput > 0)
        {
            ApplyRotation(false);
        }
        else if(rotationInput < 0)
        {
            ApplyRotation(true);
        }
    }

    /// <summary>
    /// Applies a rotational force to the spaceship
    /// </summary>
    /// <param name="moveForward">boolean that tells whether the ship is rotating to the right or left</param>
    private void ApplyRotation(bool moveForward)
    {
        rb.freezeRotation = true;
        if(moveForward)
        {
            transform.Rotate(Vector3.forward * rotationStrength * Time.fixedDeltaTime);
        }
        else
        {
            transform.Rotate(Vector3.back * rotationStrength * Time.fixedDeltaTime);
        }
    }
}
