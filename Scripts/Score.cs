using UnityEngine; // Includes the "UnityEngine" namespace.
using System.Collections; // Includes the "System.Collections" namespace.
using UnityEngine.UI; // Includes the "UnityEngine.UI" namespace.

// Class manages and provides functionality regarding the player's score.
public class Score : MonoBehaviour
{
    public int score = 0; // The player's current score.
    public GameObject TextGO = null; // GameObject information about the object containing the Text component.

    private int newLifeTarget = 100; // The target needed to gain another life.
    private Text _Text = null; // Reference to a UI script of type "Text".

    // Start gets called once the scene is loaded but after "Awake()", used for initialization.
    void Start()
    {
        _Text = TextGO.GetComponent<Text>();  // Reference to the Text script on the current GameObject so ".GetComponent<>()" doesn't get called more than once.
    }

    // Used to change the current score depending on the value inputed.
    public void ChangeScore(int newScore)
    {
        score = newScore; // Current score is updated to the value inputed.
        _Text.text = "Score: " + score; // The GUI representing the score is updated with the new score.

        // If the newlifeTarget has been met.
        if (score == newLifeTarget)
        {
            newLifeTarget += 100; // Make the new target an additional 100 points from the last target.
            this.GetComponent<Lives>().ChangeLives(this.GetComponent<Lives>().GetLives() + 1); // Add 1 life to the player's current lives.
        }
    }

}
