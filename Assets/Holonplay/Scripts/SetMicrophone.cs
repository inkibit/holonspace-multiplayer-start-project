using Photon.Pun;
using UnityEngine;
using Photon.Voice.Unity;

namespace Networking.Pun2
{
    public class SetMicrophone : MonoBehaviourPun
    {
        //For making sure that microphone is found and set to "Recorder" component from Photon Voice
        private void Start()
        {
            string[] devices = Microphone.devices;
            if (devices.Length > 0)
            {
                var recorder = GetComponent<Recorder>();
                recorder.UnityMicrophoneDevice = devices[0];

#if UNITY_ANDROID && !UNITY_EDITOR
                recorder.MicrophoneType = Recorder.MicType.Photon;
#endif
#if !UNITY_WINDOWS || UNITY_EDITOR
                recorder.MicrophoneType = Recorder.MicType.Unity;
#endif
            }
        }

    }
}