using UnityEngine; // Includes the "UnityEngine" namespace.
using System.Collections; // Includes the "System.Collections" namespace.

// Class stops the object from being destroyed. Acts as persistant storage to store the player's name.
public class Name : MonoBehaviour 
{
    public string m_name; // Stores the inputted name.

    // First funcition automatically called upon the scene loading.
    void Awake()
    {
            DontDestroyOnLoad(transform.gameObject); // Stops object from being destoryed when another scene is loaded.
    }
    
}
