using UnityEngine; // Includes the "UnityEngine" namespace.
using System.Collections; // Includes the "System.Collections" namespace.

// Class provides functionality to skip the start screen when a button is pressed. 
public class DestroyOnSpace : MonoBehaviour
{
    public MainMenu _MainMenu = null;
    public GameObject _mainMenu = null; // Stores GameObject information about the MainMenu for later use and manipulation.
    public GameObject LeftPanel = null; // Stores GameObject information about the LeftPanel for later use and manipulation.
    public GameObject RightPanel = null; // Stores GameObject information about the RightPanel for later use and manipulation.

    private bool playAnimation = true; // Later decides whether or not the panel animations are played.


    private Animation _rightPanelAnimation = null;
    private Animation _leftPanelAnimation = null;

    private void Start()
    {
        _rightPanelAnimation = RightPanel.GetComponent<Animation>();
        _leftPanelAnimation = LeftPanel.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playAnimation == false)
        {
            if(_rightPanelAnimation.isPlaying == false)
            {
                // Show the main menu.
                _MainMenu.ChangeScreenTo(_mainMenu);
            }
            return; // Only proceed if the animation should be played upon a button being pressed.
        }

        // If any button is pressed or let go.
        if (Input.anyKey)
        {
            _rightPanelAnimation.Play(); // Hide the right panel through the use of a pre-made animation. 
            _leftPanelAnimation.Play(); // Hide the left panel through the use of a pre-made animation. 
            playAnimation = false; // Do not allow the animation functionallity to be processed again.

         
        }

    }
}
