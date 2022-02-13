using Networking.Pun2;
using Photon.Pun;
using UnityEngine;

public class DestroyThis : MonoBehaviour

{
    private OVRGrabber myGrabber;

    public void destroyThis()

    {
        //this turns off the OVRGrabbable script

        this.GetComponent<PunOVRGrabbable>().enabled = false;

        //this gets the hand that's grabbing it

        myGrabber = this.GetComponent<PunOVRGrabbable>().grabbedBy;

        //use ForceRelease method in the OVRGrabber to release object

        myGrabber.ForceRelease(this.gameObject.GetComponent<PunOVRGrabbable>());

        //destroy object

        PhotonNetwork.Destroy(this.gameObject);

    }

}