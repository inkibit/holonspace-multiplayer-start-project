using Networking.Pun2;
using Photon.Pun;
using UnityEngine;

public class TriggerRecorderDeletion : MonoBehaviour
{
    [SerializeField] bool shouldBeGrabbed = true;
    [SerializeField] LayerMask layerToDelete;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == layerToDelete)
        {
            if (shouldBeGrabbed)
            {
                if(TryGetComponent(out PunOVRGrabbable grabbable))
                {
                    if (GetComponent<MakeAudioLoopObject>())
                    {
                        if (grabbable.isGrabbed)
                        {
                            grabbable.GrabEnd(Vector3.zero, Vector3.zero);
                            PhotonNetwork.Destroy(other.gameObject);
                        }
                    }
                }
            }
            else
            {
                PhotonNetwork.Destroy(other.gameObject);
            }
        }
    }
}
