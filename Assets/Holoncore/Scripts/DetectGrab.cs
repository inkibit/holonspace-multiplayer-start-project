using Chatterbug.Networking;
using Networking.Pun2;
using UnityEngine;
using UnityEngine.Events;

public class DetectGrab : MonoBehaviour
{
    public UnityEvent OnHoverEnter;
    public UnityEvent OnHoverExit;
    public UnityEvent OnGrabStartDetect;
    public UnityEvent OnGrabEndDetect;
    public UnityEvent OnUseBegin;
    public UnityEvent OnUseEnd;

    private bool grabbedLastFrame = false;
    private bool usedLastFrame = false;
    private bool handIsColliding;
    private bool handRight;
    private bool firstFrameTouching = true;

    private float grabAmount;
    private float triggerAmount;

    void Update()
    {
        if (handIsColliding)
        {
            grabAmount = 0;
            triggerAmount = 0;
            if (handRight)
            {
                grabAmount = OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger);
                triggerAmount = OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger);
            }
            else
            {
                grabAmount = OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger);
                triggerAmount = OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger);
            }

            //Check grabbing
            if (grabAmount > 0.5f && !grabbedLastFrame)
            {
                if (!firstFrameTouching)
                {
                    OnGrabStartDetect.Invoke();
                }
                else
                {
                    firstFrameTouching = false;
                }
                grabbedLastFrame = true;
            }
            bool releasedGrab = grabAmount < 0.25f && grabbedLastFrame;
            if (releasedGrab)
            {
                if (!firstFrameTouching)
                {
                    OnGrabEndDetect.Invoke();
                }
                else
                {
                    firstFrameTouching = false;
                }
                grabbedLastFrame = false;
            }

            //Check trigger/use
            if (triggerAmount > 0.75f && !usedLastFrame)
            {
                usedLastFrame = true;
                OnUseBegin.Invoke();

            }
            bool releaseTrigger = triggerAmount < 0.25f && usedLastFrame;
            if (releaseTrigger)
            {
                usedLastFrame = false;
                OnGrabEndDetect.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (handIsColliding) return;
        if (other.CompareTag("HandR"))
        {
            handIsColliding = true;
            handRight = true;
            OnHoverEnter.Invoke();
            firstFrameTouching = true;
        }
        else if (other.CompareTag("HandL"))
        {
            handIsColliding = true;
            handRight = false;
            OnHoverEnter.Invoke();
            firstFrameTouching = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!handIsColliding) return;
        if (other.CompareTag("HandR"))
        {

            handIsColliding = false;
            OnHoverExit.Invoke();
        }
        else if (other.CompareTag("HandL"))
        {
            handIsColliding = false;
            OnHoverExit.Invoke();
        }
    }
}
