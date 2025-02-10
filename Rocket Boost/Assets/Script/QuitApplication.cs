using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
///  When the escape key is pressed the game is closed
/// </summary>
public class QuitApplication : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.escapeKey.isPressed == true)
        {
            Application.Quit();
            Debug.Log("Exited Application");
        }    
    }
}
