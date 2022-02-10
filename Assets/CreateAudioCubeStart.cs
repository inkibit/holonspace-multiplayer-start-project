using Photon.Pun;
using UnityEngine;

public class CreateAudioCubeStart : MonoBehaviour
{
    [SerializeField] private GameObject audioCube;

    public void CreateRecorderObject()
    {
        PhotonNetwork.Instantiate(audioCube.name, transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(0.1f, 0.3f), Random.Range(-0.2f, 0.2f)), transform.rotation);
    }
}
