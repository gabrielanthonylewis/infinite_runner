using UnityEngine; // Includes the "UnityEngine" namespace.
using System.Collections; // Includes the "System.Collections" namespace.
using System.Collections.Generic;  // Includes the "System.Collections.Generic" namespace.
using UnityEngine.UI; // Includes the "UnityEngine.UI" namespace so that unity's UI can be used.
using System.Xml; // Includes the "System.XML" namespace so that XML can be used to save and load.

// Class deals with managing and populating the leaderboard with player data.
public class LeaderboardManagement : MonoBehaviour 
{
    private string _filePath = "";  // Stores the path to the folder, including file name which in this case is "gamedata.xml".
    private const string _format = "{0,-5} {1,-10}{2,10}{3,10}"; // The display format of the strings outputed to the text object.

    public List<GameObject> TextObjs = new List<GameObject>(); // A list of all of the GameObjects containing a text component (The empty leaderboard text objects).

    // Start gets called once the scene is loaded but after "Awake()", used for initialization.
	void Start () 
    {
        _filePath = Application.dataPath + "/Resources/GameData/gamedata.xml"; // Assigns "_filePath" with the file path of the XML file "gamedata.xml".

        XmlDocument xmlDoc = new XmlDocument(); // Creates empty xml document.
        xmlDoc.Load(_filePath); // Information regarding the game data xml file is stored into the xmlDoc variable.
        XmlElement xmlRoot = xmlDoc.DocumentElement; // The root element is created and assigned to the root element of the xml file.

        // Loops through every child node of the root element (Every player)
        for(int i = 0; i < xmlRoot.ChildNodes.Count; i++)
        {
            if (i >= TextObjs.Count) break; // Exit the for loop if there are no text objects to be filled.

            string position = (i + 1).ToString(); // Stores the position into a string. Position is i + 1 as i start at 0.
            string name = xmlRoot.ChildNodes[i].ChildNodes[0].InnerText; // Stores the name of the player which we know is the first element ([0]).
            string score = xmlRoot.ChildNodes[i].ChildNodes[1].InnerText; // Stores the player's score which we know is the second element ([1]).
            string time = xmlRoot.ChildNodes[i].ChildNodes[2].InnerText; // Sotrs the player's time which we know is the third element ([2]).

            // Sets the text inside the current text object to display the Position, Name, Score and then Time all formated from the use of the "_Format" string.
            TextObjs[i].GetComponent<Text>().text = string.Format(_format, position.PadLeft(1), name.PadLeft(10), score.PadRight(5), time.PadRight(5));
        }
	}
	
}
