using Networking.Pun2;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleBin : MonoBehaviour
{
    private List<GameObject> candidatesToDelete;

    private void Start()
    {
        candidatesToDelete = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<PunOVRGrabbable>())
        {
            var grabbable = other.GetComponentInParent<PunOVRGrabbable>();
            if (grabbable.isGrabbed)
            {
                candidatesToDelete.Add(other.gameObject);
                grabbable.OnRelease += DeleteObject;
            }
            else
            {
                DeleteObject(grabbable);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (candidatesToDelete.Contains(other.gameObject))
        {
            candidatesToDelete.Remove(other.gameObject);
            other.GetComponentInParent<PunOVRGrabbable>().OnRelease -= DeleteObject;
        }
    }

    private void DeleteObject(PunOVRGrabbable grabbable)
    {
        if (candidatesToDelete.Contains(grabbable.gameObject))
        {
            candidatesToDelete.Remove(grabbable.gameObject);
            grabbable.OnRelease -= DeleteObject;
        }
        StartCoroutine(DelayBeforeDestroying(grabbable.gameObject));
    }

    //move object and delay destruction to avoid null collider references
    IEnumerator DelayBeforeDestroying(GameObject obj)
    {
        obj.transform.position = new Vector3(0, -100, 0);
        yield return new WaitForSeconds(0.2f);
        PhotonNetwork.Destroy(obj);
    }
}
