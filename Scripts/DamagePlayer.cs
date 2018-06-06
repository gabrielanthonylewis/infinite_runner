using UnityEngine; // Includes the "UnityEngine" namespace.
using System.Collections; // Includes the "System.Collections" namespace.

// Class provides damage functionality so that the player can be damaged (lose a life) when a collision is made with an object.
public class DamagePlayer : MonoBehaviour 
{
    // Function called when an object interacts with the collider (trigger) of the object.
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;   // If anything but the player enters the collider (trigger) then return out of the fuction, else subtract 1 from the players lives.

        Lives playerLives = other.GetComponent<Lives>(); // (Optimisation) Reference to the Lives script on the player so "GetComponent<>" doesn't get called more than once.
        playerLives.ChangeLives(playerLives.GetLives() - 1); // Subtracts 1 away from the player's current lives.
    }
}
