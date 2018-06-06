using UnityEngine; // Includes the "UnityEngine" namespace.
using System.Collections; // Includes the "System.Collections" namespace.
using System.Collections.Generic; // Includes the "System.Collections.Generic" namespace.

// Class provides funcitonality regarding player movement depending on the size of the road.
public class Movement : MonoBehaviour
{
    public List<GameObject> Obstacles = new List<GameObject>(); // A list of GameObjects for all of the obstacles in the scene.
    public GameObject roadObj = null; // Gameobject information regarding the current road object the player moves on.

    private Vector3 movementIncrement = Vector3.zero; // The ammount of space to be moved upon pressing the movement keys. (calculated in Start())

    // Start gets called once the scene is loaded but after "Awake()", used for initialization.
    void Start()
    {
        GameObject newObj = Instantiate(Obstacles[0]) as GameObject; // Spawn in an obstacle object so that it's render can be accessed.
        float roadWidth = roadObj.GetComponent<Renderer>().bounds.size.x; // The road width.
		float objWidth = newObj.GetComponent<Renderer>().bounds.size.x; // The obstacles width.
        int _TotalColombs = 3; // The total amount of colombs. (Player will be able to move left, right and stay in the middle which equals 3).

        #region Desc: objGap =
        // "((roadSize.x / objSize.x)"  How many obstacles can fit on the road?
        // " - totalBatchColombs)"  how much free space is there? (e.g. if only 3 obstacles then "- 3")
        // "/ (totalBatchColombs + 1f)" devided by the ammount of gaps 
        float objGap = ((roadWidth / objWidth) - _TotalColombs) / (_TotalColombs + 1f);
        #endregion

		movementIncrement = newObj.GetComponent<Renderer>().bounds.size; // Player should move firstly the size of the obstacle,
        movementIncrement.x += objGap; // + the gap inbetween each obstacle.

        Destroy(newObj); // Destroy the object as it has been dealt with.
    }

    // Update is called once per frame
    void Update()
    {

        // (Fall) If the up arrow is let go.
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            float newPosY = Mathf.Clamp(this.transform.position.y - this.transform.up.y, 1, 2); // New position Y is on the ground.
            this.transform.position = new Vector3(this.transform.position.x, newPosY, this.transform.position.z); // Update the player's position.
        }

        // (Return to middle) If the right arrow is let go.
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            float newPosX = this.transform.position.x - movementIncrement.x; // New position is in the centre.
            this.transform.position = new Vector3(newPosX, this.transform.position.y, this.transform.position.z);  // Update the player's position.
        }

        // (Move to right side) If right arrow is pressed.
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            float newPosX = this.transform.position.x + movementIncrement.x;  // New position is to the right by one increment.
            this.transform.position = new Vector3(newPosX, this.transform.position.y, this.transform.position.z);  // Update the player's position.
        }

        // (Return to middle) If the left arrow is let go.
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            float newPosX = this.transform.position.x + movementIncrement.x;  // New position is in the centre.
            this.transform.position = new Vector3(newPosX, this.transform.position.y, this.transform.position.z);  // Update the player's position.
        }

        // (Move to left side) If left arrow is pressed.
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            float newPosX = this.transform.position.x - movementIncrement.x;  // New position is to the left by one increment.
            this.transform.position = new Vector3(newPosX, this.transform.position.y, this.transform.position.z);  // Update the player's position.
        }

        // (Jump) If up arrow is pressed.
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            float newPosY = Mathf.Clamp(this.transform.position.y + this.transform.up.y, 1, 2);  // New position is to up in the air by one increment.
            this.transform.position = new Vector3(this.transform.position.x, newPosY, this.transform.position.z);  // Update the player's position.
        }

    }
}
