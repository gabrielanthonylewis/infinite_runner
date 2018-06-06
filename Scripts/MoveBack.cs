using UnityEngine; // Includes the "UnityEngine" namespace.
using System.Collections; // Includes the "System.Collections" namespace.

// Class applys an initial velocity to the object it is attached to so that it moves backwards at a set speed.
public class MoveBack : MonoBehaviour
{
    // Start gets called once the scene is loaded but after "Awake()", used for initialization.
    void Start()
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.back * 5f; // Sets the inital velocity of the object move backwards with a speed multiplyer of 5.
    }
}
