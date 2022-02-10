using Photon.Pun;
using UnityEngine;

public class AttachToHand : MonoBehaviour
{
    [SerializeField] GameObject prefabToAttach;

    public void Attach()
    {
        var spawnedObject = PhotonNetwork.Instantiate(prefabToAttach.name, transform.position, transform.rotation);
    }
}
