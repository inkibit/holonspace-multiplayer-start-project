using System;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Networking.Pun2

{
    public class OnUseEvent : MonoBehaviourPun


    {

        public UnityEvent OnUseEnter;
        public UnityEvent OnUseExit;

        private bool pressedLastFrame = false;
        private bool clonedLastFrame = false;
        private string myPrefabName;
        private Transform thisObjectTransform;
        private PunOVRGrabbable grabbable;

        private void Start()
        {
            grabbable = GetComponent<PunOVRGrabbable>();
        }

        // Update is called once per frame
        void Update()
        {

            float triggerAmount = 0;

            if (photonView.IsMine)
            {
                if (grabbable.isGrabbed)
                {
                    triggerAmount = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

                    if (triggerAmount > 0.75f)
                    {
                        pressedLastFrame = true;

                        OnUseEnter.Invoke();
                    }

                    bool releasedTrigger = triggerAmount < 0.25f && pressedLastFrame;

                    if (releasedTrigger)
                    {
                        pressedLastFrame = false;
                        clonedLastFrame = false;
                        OnUseExit.Invoke();
                    



                    }
                }

            }
        }
    }
}
