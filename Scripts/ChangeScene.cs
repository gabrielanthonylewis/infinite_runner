using UnityEngine; // Includes the "UnityEngine" namespace.
using System.Collections; // Includes the "System.Collections" namespace.
using UnityEngine.UI; // Includes the "UnityEngine.UI" namespace.

// Class allows the scene/level to be changed. For example, from the Main menu to the Game.
public class ChangeScene : MonoBehaviour
{
    public GameObject text = null;  // Stores the GameObject details of the text object for later use.
    public GameObject nameHolder = null;  // Stores the GameObject details of the nameHolder object for later use (saving).
    
    // "ChangeSceneTo" function changes the scene/level depending on "int SceneNumber"
    public void ChangeSceneTo(int SceneNumber)
    {
        // If the scene is being changed to the Game(1) scene then store the name inputted by the user by accessing .
        if(SceneNumber == 1) 
            nameHolder.GetComponent<Name>().name = text.GetComponent<Text>().text;
        
        Application.LoadLevel(SceneNumber); // Changes scene depending on the integar "SceneNumber" that has been entered.
    }

}
