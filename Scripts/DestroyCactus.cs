using UnityEngine; // Includes the "UnityEngine" namespace.
using System.Collections; // Includes the "System.Collections" namespace.
using UnityEngine.UI; // Includes the "UnityEngine.UI" namespace.

// Class provides fuctionality concerned with the destruction of the cactus obstacle.
public class DestroyCactus : MonoBehaviour
{
    private Score _Score = null; // (Optimisation) 

    // Start gets called once the scene is loaded but after "Awake()", used for initialization.
    void Start()
    {
        _Score = this.GetComponent<Score>(); // Reference to the Score script on the player so "GetComponent<>" doesn't get called more than once.
    }

    // Update is called once per frame
    void Update()
    {
        // If the "Space" key is pressed on the keyboard.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit hit; // Stores the information about the raycast that has hit an object.

            // Generate a raycast from the player's position poiting straight forward for a distance of 2 units.
            if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
            {
                if (hit.transform.tag != "Cactus") return; // Only proceed if the hit object is a cactus.

                Destroy(hit.collider.gameObject); // Destroys the object infront on the player (first object hit by the raycast).
                _Score.ChangeScore(_Score.score -= 10); // Subtracts 10 fromt the player's current score.
            }
        }

    }
}
