using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrapSnap : MonoBehaviour
{
    // The OVRGrabbable component attached to the object
    private OVRGrabbable grabbable;

    // The degrees to which the object should snap when grabbed
    public float snapDegrees = 45f;

    void Start()
    {
        // Get the OVRGrabbable component attached to the object
        grabbable = GetComponent<OVRGrabbable>();
    }

    void Update()
    {
        // If the object is currently being grabbed
        if (grabbable.isGrabbed)
        {
            // Get the current rotation of the object
            Quaternion currentRotation = transform.rotation;

            // Calculate the snapped rotation by rounding the current rotation to the nearest snapDegrees value
            Quaternion snappedRotation = Quaternion.Euler(
                Mathf.Round(currentRotation.eulerAngles.x / snapDegrees) * snapDegrees,
                Mathf.Round(currentRotation.eulerAngles.y / snapDegrees) * snapDegrees,
                Mathf.Round(currentRotation.eulerAngles.z / snapDegrees) * snapDegrees
            );

            // Set the rotation of the object to the snapped rotation
            transform.rotation = snappedRotation;
        }
    }
}