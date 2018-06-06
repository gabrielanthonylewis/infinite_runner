using UnityEngine; // Includes the "UnityEngine" namespace.
using System.Collections; // Includes the "System.Collections" namespace.

// Class deals with repositioning an object to the related ground that has been assigned.
public class RelatedGround : MonoBehaviour
{
    public GameObject relatedGroundOBJ = null; // GameObject information about the related ground where the object will be repositioned behind. 

    // Reposition the object behind the related ground object that has been assigned.
    public void Reposition()
    {
        if (relatedGroundOBJ == null) return; // If the related ground object is empty, return to prevent further processing.

        Vector3 newPos = relatedGroundOBJ.transform.position; // newPos represents the new position. Initliase it to be the related grounds position.
        newPos.z -= -relatedGroundOBJ.transform.localScale.z; // new position will be behind the related object (newPos.z - the depth of the related ground object).
        this.transform.position = newPos; // Move the object to the new position.

        this.GetComponent<SpawnObjects>().Spawn(25, 3); // Spawn a new set of obstacles on the newly positioned ground.
    }
}
