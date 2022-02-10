using Photon.Voice.Unity;
using UnityEngine;

public class PersistentReferences : MonoBehaviour
{
    public VoiceConnection voiceConnection;
    public static PersistentReferences instance;

    private void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
