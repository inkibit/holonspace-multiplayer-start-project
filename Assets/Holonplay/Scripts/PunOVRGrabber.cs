using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Networking.Pun2
{
    [RequireComponent (typeof (PhotonView))]
    public class PunOVRGrabber : OVRGrabber
    {
        PhotonView pv;
        protected override void Awake()
        {
            base.Awake();
            pv = GetComponent<PhotonView>();
        }

        //Basically, if photonview is mine, update anchors from Grabber
        public override void Update()
        {
            if(pv.IsMine)
                base.Update();
        }

        protected override void OffhandGrabbed(OVRGrabbable grabbable)
        {
            if (m_grabbedObj == grabbable)
            {
                if (grabbable.allowOffhandGrab)
                {
                    GrabbableRelease(Vector3.zero, Vector3.zero);
                }
            }
        }
    }
}
