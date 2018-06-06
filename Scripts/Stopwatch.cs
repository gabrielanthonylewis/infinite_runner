using UnityEngine; // Includes the "UnityEngine" namespace.
using System.Collections; // Includes the "System.Collections" namespace.
using UnityEngine.UI; // Includes the "UnityEngine.UI" namespace.

// Class provides stopwatch functionalilty and manages the text display.
public class Stopwatch : MonoBehaviour
{
    private string _strExtra = "0"; // For formatting purposes. An extra 0 will be needed if minutes if below 10.
    private string _strExtraMin = "0"; // For formatting purposes. An extra 0 will be needed if minutes if below 10.
    private Text _Text = null; // Reference to a UI script of type "Text".

    public float _Seconds = 00f; // Stores the current amount of seconds.
    public float _Minutes = 00f; // Sotres the current amount of minutes.

    // Start gets called once the scene is loaded but after "Awake()", used for initialization.
    void Start()
    {
        _Text = this.GetComponent<Text>(); // Reference to the Text script on the current GameObject so ".GetComponent<>()" doesn't get called more than once.
    }

    // Update is called once per frame
    void Update()
    {
        _Seconds += Time.deltaTime; // _Seconds variable updated every second.

        // If 60 or more seconds have passed.
        if (_Seconds >= 60f)
        {
            _Minutes++; // Increase the minutes by 1.
            _Seconds = 0f; // Reset the seconds to 0.
        }
        
        // If the amount of seconds is less than 10.
        if (Mathf.Round(_Seconds) < 10f)
            _strExtra = "0"; // Add a 0 so the display is for example: 00:09.
        else // Else if the seconds is greater than or equal to 10.
            _strExtra = ""; // Remove the 0 so the display is for example: 00:10.

        // If the amount of minutes is less than 10.
        if (Mathf.Round(_Minutes) < 10f)
            _strExtraMin = "0"; // Add a 0 so the display is for example: 09:00.
        else // Else if the minutes is greater than or equal to 10.
            _strExtraMin = ""; // Remove the 0 so the display is for example: 10:00.

        _Text.text = _strExtraMin + _Minutes + ":" + _strExtra + _Seconds.ToString("F0"); // Text is updated with the minutes, seconds and formating variables. The seconds are formated so that the seconds arent too accurate, e.g. 00:0123423 becomes 00:01.
    }

}
