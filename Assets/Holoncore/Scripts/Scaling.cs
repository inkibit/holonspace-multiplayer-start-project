using Chatterbug.Networking;
using Networking.Pun2;
using UnityEngine;
using UnityEngine.Events;

public class Scaling : MonoBehaviour
{
    private float grabAmountR;
    private float grabAmountL;

    public bool bothHandsGrabbing;

    public Transform RHand;
    public Transform LHand;

    void Update()
    {
        if (RHand != null && LHand != null)
        {
            grabAmountR = 0; grabAmountL = 0;

            grabAmountR = OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger);
            grabAmountL = OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger);
            
            //Check grabbing
            if (grabAmountR > 0.5f && grabAmountL > 0.5f)
            {
                bothHandsGrabbing = true;
            }
            else
            {
                bothHandsGrabbing = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HandR"))
        {
            RHand = other.transform;
        }
        else if (other.CompareTag("HandL"))
        {
            LHand = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HandR"))
        {
            RHand = null;
        }
        else if (other.CompareTag("HandL"))
        {
            LHand = null;
        }
    }
}
