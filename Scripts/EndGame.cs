using UnityEngine; // Includes the "UnityEngine" namespace.
using System.Collections; // Includes the "System.Collections" namespace.
using System.Xml; // Includes the "System.XML" namespace so that XML can be used to save and load.
using System.IO; // Includes the "System.IO" namespace so that files and directories can be created and manipulated.
using UnityEngine.UI; // Includes the "UnityEngine.UI" namespace so that unity's UI can be used.

// Class deals with saving the user's name, score, time and position and also exiting the game.
public class EndGame : MonoBehaviour
{
    private string _folderPath = ""; // Stores the path to the folder "/GameData" where the player's information will be stored.
    private string _filePath = "";  // Stores the path to the folder, including file name which in this case is "gamedata.xml".

    public GameObject ScoreObject = null; // Stores GameObject information about the ScoreObject for later use and manipulation (storing the player's score).
    public GameObject TimeObject = null; // Stores GameObject information about the MainMenu for later use and manipulation (storing the player's time).
    public GameObject NameObject = null; // Stores GameObject information about the MainMenu for later use and manipulation (storing the player's name).

    // Start gets called once the scene is loaded but after "Awake()", used for initialization.
    void Start()
    {
        _folderPath = Application.dataPath + "/Resources/GameData"; // Assigns "_folderPath" with the folder path of the folder "/GameData".
        _filePath = Application.dataPath + "/Resources/GameData/gamedata.xml"; // Assigns "_filePath" with the file path of the file "gamedata.xml".

        // If the folder doesn't exist.
        if (!Directory.Exists(_folderPath))
            Directory.CreateDirectory(_folderPath); // Create the folder in the assigned directory.

        // If the "gamedata.xml" file doesnt exist, create it in the assigned directory. (Note: second parameter is the information about the directory so it can be searched)
        if (!CheckFile("gamedata.xml", new DirectoryInfo(_folderPath).GetFiles()))
            if (!CreateFile(_filePath)) Debug.Log("Error: File wasn't created"); // Output error to log if the file failed to be created.
        
        NameObject = GameObject.FindGameObjectWithTag("NameHolder"); // Assigns "NameObject" with the GameObject information of the "NameHolder" object that has been searched for (search method is via the objects tag).  
    }

    // Once called, the game will data will be saved and the game will end and therefore, the user will be taken back to the main menu.
    public void End()
    {
        SaveToFile(_filePath); // Saves the player's data.
        Destroy(NameObject); // The "NameObject" (NameHolder) object is destroyed as the main menu has it's own persistant NameHolder object.
        Application.LoadLevel(0); // Player is taken to the main menu.
    }

    // All of the player's data will get stored to an xml.
    private void SaveToFile(string filePath)
    {
        XmlDocument xmlDoc = new XmlDocument(); // Creates empty xml document.
        xmlDoc.Load(filePath); // Information regarding the game data xml file is stored into the xmlDoc variable.
        XmlElement xmlRoot = xmlDoc.DocumentElement; // The root element is created and assigned to the root element of the xml file.

        float score = ScoreObject.GetComponent<Score>().score; // Store the player's score.
        string name = "null"; // Will store the player's name. Assigned to "null" incase a name is not entered during debugging etc.

        // If the NameObject exists and is stored, store the player's name.
        if (NameObject != null) name = NameObject.GetComponent<Name>().name;

        string time = TimeObject.GetComponent<Text>().text; // Store the player's time.
        float timef = TimeObject.GetComponent<Stopwatch>()._Minutes + TimeObject.GetComponent<Stopwatch>()._Seconds; // Retrieve the minutes and seconds from the stopwatch itself to store the time as a float for future sorting.

        XmlElement elmPlayerID = xmlDoc.CreateElement("PlayerID_" + (this.GetInstanceID())); // Create the "PlayerID" element. This will store all of the information about the current player.
        xmlRoot.AppendChild(elmPlayerID); // Append the element inside the "PlayerID_" element.

        XmlElement elmPlayerName = xmlDoc.CreateElement("Name"); // Create the "Name" element to store the the players name.
        elmPlayerName.InnerText = name; // Player's name stored in the element.
        elmPlayerID.AppendChild(elmPlayerName); // "Name" element appended inside the "PlayerID_" element. 

        XmlElement elmPlayerScore = xmlDoc.CreateElement("Score"); // Create the "Score" element to store the the players score.
        elmPlayerScore.InnerText = score.ToString(); // Player's score stored in the element.
        elmPlayerID.AppendChild(elmPlayerScore);  // "Score" element appended inside the "PlayerID_" element. 

        XmlElement elmPlayerTime = xmlDoc.CreateElement("Time"); // Create the "Time" element to store the the players time.
        elmPlayerTime.InnerText = time; // Player's time stored in the element.
        elmPlayerID.AppendChild(elmPlayerTime); // "Time" element appended inside the "PlayerID_" element. 

        // Loops through every element containing user information (Already know is the root's child nodes).
        for (int i = 0; i < xmlRoot.ChildNodes.Count; i++)
        {
            float score1 = 0f; // Will store the current old score stored in the file depending on the value of i.
            float.TryParse(xmlRoot.ChildNodes[i].ChildNodes[1].InnerText, out score1); // Converts the score string from the xml file (depending on the value of i) to a float and then stores it to "score1".

            // If the player's score is greater than the current old, other player's current score.
            if (score > score1) 
            {
                xmlRoot.InsertBefore(elmPlayerID, xmlRoot.ChildNodes[i]); // Insert the player's element and data before the other player's.
                break; // End the loop.
            }

            float minutes1 = 0f; // Will store the current old minutes stored in the file depending on the value of i.
            string minutes1STR = (xmlRoot.ChildNodes[i].ChildNodes[2].InnerText.Substring(0, 2)); // Stores the minutes from the current old time (depending on the value of i). A substring is required as the whole time is stored and not just the minutes.
            float.TryParse(minutes1STR, out minutes1); // Converts the minutes string from the xml file to a float and then stores it to "minutes1".

            float seconds1 = 0f; // Will store the current old seconds stored in the file depending on the value of i.
            string seconds1STR = (xmlRoot.ChildNodes[i].ChildNodes[2].InnerText.Substring(3, 2)); // Stores the seconds from the current old time (depending on the value of i). A substring is required as the whole time is stored and not just the seconds.
            float.TryParse(seconds1STR, out seconds1); // Converts the seconds string from the xml file to a float and then stores it to "seconds1".

            float time1 = minutes1 + seconds1; // The current old full time is stored as a float by adding the current old minutes to the current old seconds.

            // If the player's score is equal to the current old player's score.
            if(score == score1)
            {
                // If the player's time is less than or equal to the current old player's score.
                if(timef <= time1)
                {
                    xmlRoot.InsertBefore(elmPlayerID, xmlRoot.ChildNodes[i]); // Insert player's data before the current old player stored on the xml file.
                    break; // End the loop.
                }
                
                // If the loop is at the last element.
                if(i == xmlRoot.ChildNodes.Count)
                {
                    xmlRoot.InsertAfter(elmPlayerID, xmlRoot.ChildNodes[i]); // Insert player's data after the last player stored on the xml file.
                    break; // End the loop.
                }
            }
           
        }

        xmlDoc.Save(filePath); // Saves the new changes to the xml file.
    }

    // Creates the file depending on the directory given.
    private bool CreateFile(string filePath)
    {
        System.IO.File.WriteAllText(filePath, "<Players>" + "\n" + "</Players>"); // Creates the new file and writes the inital xml structure for later storage of player data.

        XmlDocument xmlDoc = new XmlDocument(); // Creates empty xml document.
        xmlDoc.Load(filePath); // Information regarding the game data xml file is stored into the xmlDoc variable.
        XmlElement elmRoot = xmlDoc.DocumentElement; // The root element is created and assigned to the root element of the xml file.
        elmRoot.RemoveAll(); // Removes all of the data inside the root element.

        xmlDoc.Save(filePath); // Saves the new changes to the xml file.
        return true; // Return true signaling that the file has been created properly.
    }

    // Checks whether the file exists in the folder files given as "FileInfo[]".
    private bool CheckFile(string filename, FileInfo[] fileInfo)
    {
        // Loop through all of the files information stored in "fileInfo".
        for (int i = 0; i < fileInfo.Length; i++)
        {
            // If the current file name is equal to the file name inputted.
            if (fileInfo[i].Name == filename)
                return true; // Return try signaling that the file exists.
        }
        return false; // Return try signaling that the file doesn't exist.
    }
}
