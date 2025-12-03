using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagCheckDestroy : MonoBehaviour
{
    // Set this to the tag you want to allow (example: "AllowedItem")
    public string requiredTag = "Untagged";

    private void OnTriggerEnter(Collider other)
    {
        // If the object does NOT have the required tag → remove it
        if (other.CompareTag(requiredTag))
        {
            Destroy(other.transform.root.gameObject);
        }
    }
}
