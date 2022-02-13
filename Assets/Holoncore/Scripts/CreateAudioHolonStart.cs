using Photon.Pun;
using UnityEngine;

public class CreateAudioHolonStart : MonoBehaviour
{
    [SerializeField] private GameObject audioHolon;
    [SerializeField] private Transform spawnPoint;

    public void CreateRecorderObject()
    {
        PhotonNetwork.Instantiate(audioHolon.name, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }
}
