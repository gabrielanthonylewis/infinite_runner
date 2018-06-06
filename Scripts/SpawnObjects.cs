using UnityEngine; // Includes the "UnityEngine" namespace.
using System.Collections; // Includes the "System.Collections" namespace.
using System.Collections.Generic; // Includes the "System.Collections.Generic" namespace.

// Class provides functionality to spawn objects onto a road.
public class SpawnObjects : MonoBehaviour
{
    public List<GameObject> Obstacles = new List<GameObject>(); // List of Gameobject information about the different types of obstacles.
    public List<GameObject> SpawnedObstacles = new List<GameObject>(); // Gameobject information keeping track of the spawned obstacles for later deletion.
    public GameObject Coin = null; // Gameobject information about the Coin which will be later spawned.

    // Start gets called once the scene is loaded but after "Awake()", used for initialization.
    void Start()
    {
        this.GetComponent<MoveBack>().enabled = false; // Stationary so the objects can be spawned and move with the road.
        Spawn(25, 3); // Initially spawn the objects.
        this.GetComponent<MoveBack>().enabled = true; // Move backwards.
    }

    // Spawn obstacles depending on the rows and colombs inputed.
    public void Spawn(int totalRows, int totalColombs)
    {
        // Loop through all of the spawned objects.
        for (int i = 0; i < SpawnedObstacles.Count; i++)
        {
            if (SpawnedObstacles[i] != null) // If an object exists,
                Destroy(SpawnedObstacles[i].gameObject); // Delete it.
        }
        SpawnedObstacles.Clear(); // Empty/clear list.

        Vector3 roadSize = this.GetComponent<Renderer>().bounds.size; // The size of the road.
        float farLeftPoint = this.transform.position.x - roadSize.x / 2f; // Returns the far left hand point of the GROUND object.
        GameObject newObjPrefab = null; // The new object prefab.
        float halfObjWidth = 0f; // Half of the object's width.
        Vector3 objSize = Vector3.zero; // The object's size (width, depth and height).

        int totalObjBatches = 2; // Amount of object batches.
        int totalGaps = 3; // Amount of gaps.
        int totalBatchRows = 6; // Amount of rows in a batch.
        int totalBatchColombs = 3; // Amound of colombs in a batch.

        // Loop through each batch of object's.
        for (int currBatch = 0; currBatch < (totalObjBatches + totalGaps); currBatch++)
        {
            bool[,] batchGridMatrix = new bool[totalBatchRows, totalBatchColombs]; // 2D Grid Representation of the current batch.

            // If the current batch is a gold collection batch.
            if (currBatch == 0 || currBatch == 2 || currBatch == 4)
                newObjPrefab = Coin; // New object should be a coin.
            else // Otherwise.
            {
                int randIndex = Random.Range(0, 101); // Choose a random index to decide on which object will spawn.

                // If the index is <= 25.
                if (randIndex <= 25)
                    newObjPrefab = Obstacles[1]; // Spawn the first object (Quicksand).
                else if (randIndex > 25 && randIndex <= 50) // If the index is > 25 and less than or equal to 50.
                    newObjPrefab = Obstacles[2]; // Spawn the third object (Sand).
                else if (randIndex > 50) // If the index is > 50. (Biggest chance of spawning)
                    newObjPrefab = Obstacles[0]; // Spawn first object (Cactus).
            }

            if (newObjPrefab == null) continue; // Skip to next increment of loop if object is null.

            for (int currBatchRow = 0; currBatchRow < Random.Range(4, totalBatchRows + 1); currBatchRow++)
            {
                int currColomb = Random.Range(0, 3); // Pick a random colomb between 0 and 3,
                batchGridMatrix[currBatchRow, currColomb] = true; // Set that colomb to true on the row so that it will be spawned as an object.
            }

            objSize = newObjPrefab.GetComponent<Renderer>().bounds.size; // The width, heigh and length of the object.
            halfObjWidth = (newObjPrefab.GetComponent<Renderer>().bounds.size.x / 2f);   // Half of the objects width so the object isn't centered to the left hand point of the ground.

            // (Instantiate Batch of obstacles) Loop through each row.
            for (int currRow = 0; currRow < totalBatchRows; currRow++)
            {
                // Loop through each colomb.
                for (int currColomb = 0; currColomb < totalBatchColombs; currColomb++)
                {
                    // If there is an empty space / no obstacle.
                    if (batchGridMatrix[currRow, currColomb] == false)
                        continue; // Continue to the next colomb.

                    GameObject newObj = Instantiate(newObjPrefab) as GameObject; // Spawn the new object.
                    SpawnedObstacles.Add(newObj); // Add the new object to the "SpawnedObstacles" List so the object can be tracked and deleted later.

                    Vector3 newPos = Vector3.zero; // The new position which the object will later be moved to.

                    #region Desc: objGap =
                    // "((roadSize.x / objSize.x)"  How many obstacles can fit on the road?
                    // " - totalBatchColombs)"  how much free space is there? (e.g. if only 3 obstacles then "- 3")
                    // "/ (totalBatchColombs + 1f)" devided by the ammount of gaps 
                    #endregion
                    float objGap = ((roadSize.x / objSize.x) - totalBatchColombs) / (totalBatchColombs + 1f);          

                    #region Desc: newPos.x =
                    // "farLeftPoint + halfObjWidth" the objects postion when it fits att he far left hand point. (Object centre + half of object)
                    // "+ objGap" is the gap between objects.
                    // "+ currColomb" is required so that the object is positioned dependent on the colomb. e.g. [obj][obj][obj] (3 colombs)
                    // "(+ currColomb * objGap)" + gap depending on the current colomb.       
                    #endregion
                    newPos.x = farLeftPoint + halfObjWidth + objGap + currColomb + (currColomb * objGap);

                    #region Desc: newPos.y =
                    // "0.5 *" a half of 
                    // "(1 + newObj.renderer.bounds.size.y)" 1m + object's height.
                    #endregion
                    newPos.y = 0.5f * (1f + newObj.GetComponent<Renderer>().bounds.size.y);

                    #region Desc: newPos.z =
                    // "(this.transform.position.z - Mathf.Floor(this.renderer.bounds.size.z / 2f))"  returns the bottom point of the ground object.
                    // "+ currRow" used so the object is placed depending on the current row number.
                    #endregion
                    newPos.z = (this.transform.position.z - Mathf.Floor(roadSize.z / 2f)) + currRow + currBatch * 5;

                    newObj.transform.position = newPos; // Update objects position to the new position.
                }
            }
        }
    }

}
