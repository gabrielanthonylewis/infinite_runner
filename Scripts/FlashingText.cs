using UnityEngine; // Includes the "UnityEngine" namespace.
using System.Collections; // Includes the "System.Collections" namespace.
using UnityEngine.UI; // Includes the "UnityEngine.UI" namespace.

// Class provides functionality to make GUI Text flash.
public class FlashingText : MonoBehaviour
{
    Text _Text; // Reference to a UI script of type "Text".
    bool hideText = true; // When true, the text should fade. When false, the text should show.

    // Start gets called once the scene is loaded but after "Awake()", used for initialization.
    void Start()
    {
        _Text = this.GetComponent<Text>(); // Reference to the Text script on the this current GameObject so ".GetComponent<>()" doesn't get called more than once.
    }

    // Update is called once per frame
    void Update()
    {
        // If the Text is hidden (Alpha channel is 0)
        if (_Text.color.a == 0f)
            hideText = false; // Set hideText to false to begin showing the text.
        else if (_Text.color.a >= 1f) // Else if Text is shown (Alpha channel is >= 1)
            hideText = true; // Set hideText to true to begin hiding the text.

        Color newColour = _Text.color; // newColour will be the new colour that is assigned to the text.
        if (hideText) // If hideText is true.
            newColour.a = Mathf.MoveTowards(newColour.a, 0f, Time.deltaTime); // Decrease alpha towards 0f over time.
        else if(!hideText) // Else if hideText is false;
            newColour.a = Mathf.MoveTowards(newColour.a, 255f, Time.deltaTime); // Increase alpha towards 255f over time.

        _Text.color = newColour; // Set the text's colour to the new colour.
    }
}
