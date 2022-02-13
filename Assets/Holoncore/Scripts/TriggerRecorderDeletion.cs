using Networking.Pun2;
using Photon.Pun;
using UnityEngine;

public class TriggerRecorderDeletion : MonoBehaviour
{
    [SerializeField] LayerMask layerToDelete;

    private void OnTriggerEnter(Collider other)
    {
        if ((layerToDelete & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
        {
            if(other.GetComponentInParent<MakeAudioLoopObject>())
            {
                if (other.GetComponentInParent<PunOVRGrabbable>())
                {
                    var grabbable = GetComponentInParent<PunOVRGrabbable>();
                    if (grabbable.isGrabbed)
                    {
                        grabbable.grabbedBy.ForceRelease(grabbable);
                        PhotonNetwork.Destroy(other.gameObject.GetComponentInParent<PhotonView>().gameObject);
                    }
                }
            }
        }
    }
}
