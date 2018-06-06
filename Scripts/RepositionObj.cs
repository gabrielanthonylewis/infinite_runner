using UnityEngine; // Includes the "UnityEngine" namespace.
using System.Collections; // Includes the "System.Collections" namespace.

// Class handles the repositioning of objects that have entered the trigger.
public class RepositionObj : MonoBehaviour
{
    // When an object enters the trigger.
    void OnTriggerEnter(Collider other)
    {
        // If the entered object has a "RelatedGround" objec then it can be repositioned.
        if (other.GetComponent<RelatedGround>())
            other.GetComponent<RelatedGround>().Reposition();   // Call the "Reposition()" function on the object for it to be repositioned.
    }
}
