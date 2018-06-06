using UnityEngine; // Includes the "UnityEngine" namespace.
using System.Collections; // Includes the "System.Collections" namespace.
using UnityEngine.UI; // Includes the "UnityEngine.UI" namespace.

// Class provides functionality regarding the player's lives.
public class Lives : MonoBehaviour
{
    public GameObject Manager = null; // Stores GameObject data about the manager object for later use.
    public GameObject TextGO = null; // Stores GameObject data about the object contains the "Text" component for later use.

    private int lives = 3; // The player's current lives.
    private Text _Text = null; // Reference to a UI script of type "Text".

    // Start gets called once the scene is loaded but after "Awake()", used for initialization.
    void Start()
    {
        _Text = TextGO.GetComponent<Text>();  // Reference to the Text script on the "TextGO" GameObject so ".GetComponent<>()" doesn't get called more than once.
    }

    // Script changes the player's current lives depending on the value inputted.
    public void ChangeLives(int newLives)
    {
        lives = newLives; // Sets the player's current lives so the value of "newLives".
        _Text.text = lives.ToString();

        // If the player's current lives is less than or equal to 0.
        if (lives <= 0)
            Manager.GetComponent<EndGame>().End();   // Ends the game (Will save during end function).
    }

    // Returns the ammount of lives the player has.
    public int GetLives()
    {
        return lives; // Returns the value of "lives".
    }
}
