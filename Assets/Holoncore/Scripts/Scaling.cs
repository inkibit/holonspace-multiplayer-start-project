using Chatterbug.Networking;
using Networking.Pun2;
using UnityEngine;
using UnityEngine.Events;

public class Scaling : MonoBehaviour
{
    bool lastFrameGrabbingR;
    bool lastFrameGrabbingL;

    private float grabAmountR;
    private float grabAmountL;

    private bool isScaling;

    public Transform RHand;
    public Transform LHand;

    private float initialHandDistance;

    private PunOVRGrabbable grabbable;
    private Vector3 grabbableInitialScale;

    private void Start()
    {
        grabbable = GetComponent<PunOVRGrabbable>();
    }

    void Update()
    {
        if (RHand != null && LHand != null)
        {
            grabAmountR = 0; grabAmountL = 0;

            grabAmountR = OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger);
            grabAmountL = OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger);
            bool grabR = false;
            bool grabL = false;

            //Check grabbing
            if (grabAmountR > 0.6f)
            {
                grabR = true;
            }
            else if(grabAmountR < 0.3)
            {
                grabR = false;
            }

            if(grabAmountL > 0.6f)
            {
                grabL = true;
            }
            else if (grabAmountL < 0.3)
            {
                grabL = false;
            }

            if (grabR && grabL)
            {
                if (grabbable.isGrabbed && !isScaling)
                {
                    Debug.Log("Start Scaling");
                    isScaling = true;
                    initialHandDistance = Vector3.Distance(LHand.position, RHand.position);
                    grabbableInitialScale = grabbable.transform.localScale;
                }
            }
            else
            {
                if (isScaling)
                {
                    Debug.Log("Stop Scaling");
                    isScaling = false;
                }
            }

            if (!grabR)
            {
                lastFrameGrabbingR = false;
            }
            if (!grabL)
            {
                lastFrameGrabbingL = false;
            }
        }
        else if (isScaling)
        {
            Debug.Log("Stop Scaling");
            isScaling = false;
        }

        if (isScaling && LHand && RHand)
        {
            float scaleDiff = Vector3.Distance(LHand.position, RHand.position) - initialHandDistance;
            Vector3 scaleDiffV = new Vector3(scaleDiff, scaleDiff, scaleDiff);
            grabbable.transform.localScale = grabbableInitialScale + scaleDiffV * grabbable.transform.localScale.x * 5;
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
