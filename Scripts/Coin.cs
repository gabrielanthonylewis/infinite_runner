using UnityEngine; // Includes the "UnityEngine" namespace.
using System.Collections; // Includes the "System.Collections" namespace.

// Class provides coin functionality such as being collected by the player.
public class Coin : MonoBehaviour
{
    // Function called when an object interacts with the collider (trigger) of the coin.
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag != "Player") return;  // If anything but the player enters the collider (trigger) then return out of the fuction, else add 1 to the total score and delete the coin.

        Score playerScore = other.GetComponent<Score>(); // (Optimisation) Reference to the Score script on the player so "GetComponent<>" doesn't get called more than once.
        playerScore.ChangeScore(playerScore.score + 1); // Adds 1 to the current score.
        Destroy(this.gameObject); // Deletes the coin.
    }
}
