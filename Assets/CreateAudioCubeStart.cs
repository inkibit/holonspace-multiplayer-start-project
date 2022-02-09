using Photon.Pun;
using UnityEngine;

public class CreateAudioCubeStart : MonoBehaviour
{
    [SerializeField] private GameObject audioCube;

    void Start()
    {
        PhotonNetwork.Instantiate(audioCube.name, transform.position, transform.rotation);
    }
}
