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
            if(GetComponentInParent<PunOVRGrabbable>())
            {
                var grabbable = GetComponentInParent<PunOVRGrabbable>();
                if (GetComponentInParent<MakeAudioLoopObject>())
                {
                    if (grabbable.isGrabbed)
                    {
                        grabbable.GrabEnd(Vector3.zero, Vector3.zero);
                        PhotonNetwork.Destroy(other.gameObject.GetComponentInParent<PhotonView>().gameObject);
                    }
                }
            }
            else
            {
                PhotonNetwork.Destroy(other.gameObject.GetComponentInParent<PhotonView>().gameObject);
            }
        }
    }
}
