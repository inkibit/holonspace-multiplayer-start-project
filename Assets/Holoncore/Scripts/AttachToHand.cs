using Photon.Pun;
using UnityEngine;

namespace Networking.Pun2
{

    public class AttachToHand : MonoBehaviour

    {

        [SerializeField] GameObject prefabToAttach;
        [SerializeField] GameObject attachPoint;

        public void Attach()
        {
            var spawnedObject = PhotonNetwork.Instantiate(prefabToAttach.name, attachPoint.transform.position, transform.rotation);
        }
    }
}
