using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.IO;
using System;
using Photon.Pun;
using UnityEngine.Networking;
using Photon.Voice.Unity;
using Networking.Pun2;
using Photon.Voice.PUN;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class MakeAudioLoopObject : MonoBehaviourPun
{
    //public float loudness;
    //public float sensitivity;
    //public float minScale;
    private string _SelectedDevice;
    private string[] devices;
    static float[] samplesData;
    private bool recording;
    private bool generated;
    private string filename;
    private string filepath;
    private int audioCreator;
    private Recorder photonRecorder;

    [Tooltip("If non assigned, look for prefab in Resources folder")]
    [SerializeField] private GameObject looperRecorder;
    //private string packageName;
    //public Text pathtext;
    //public float x, y, z;
    // Start is called before the first frame update

    private AudioSource audioSource;
    private Recorder recorder;
    private MeshRenderer meshRenderer;
    private SkinnedMeshRenderer skinnedMeshRenderer;

    [Header("AudioSettings")]
    [SerializeField] int loopDuration;
    [SerializeField] Color duringRecordingColor = StaticValues.ColorInkipink;
    [SerializeField] Color postRecordingColor = Color.white;
    private Color defaultColor;
    [SerializeField] float minDistance = 0.5f;
    [SerializeField] float maxDistance = 10f;
    [SerializeField] float spatialBlend = 1f;

    private void Awake()
    {
        recording = false;
        generated = false;

        if (GetComponentInChildren<MeshRenderer>())
        {
            meshRenderer = GetComponentInChildren<MeshRenderer>();
            defaultColor = meshRenderer.material.color;
        }
        if(GetComponentInChildren<SkinnedMeshRenderer>())
        {
            skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
            defaultColor = skinnedMeshRenderer.material.color;
        }
        if (postRecordingColor == Color.white)
        {
            postRecordingColor = defaultColor;
        }
    }

    void Start()
    {
        //sensitivity = 100;
        loopDuration = 4;
        //minScale = 0.5f;
        //packageName = "com." + Application.companyName + "." + Application.productName;
        //Debug.Log(packageName);
        InitializeAudioLooper();
    }

    //If we create this shell, instantiate the recorder child and set it as a child
    private void InitializeAudioLooper()
    {
        if (photonView.IsMine)
        {
            var obj = PhotonNetwork.Instantiate(looperRecorder.name, transform.position, transform.rotation);
            photonView.RPC("RPC_InitializeAudioLooper", RpcTarget.AllBuffered, obj.GetComponent<PhotonView>().ViewID);
        }
    }

    [PunRPC]
    public void RPC_InitializeAudioLooper(int photonViewID)
    {
        audioSource = PhotonView.Find(photonViewID).GetComponentInChildren<AudioSource>();
        recorder = PhotonView.Find(photonViewID).GetComponentInChildren<Recorder>();

        PhotonView.Find(photonViewID).transform.SetParent(transform);
        audioSource.maxDistance = maxDistance;
        audioSource.minDistance = minDistance;
        audioSource.spatialBlend = spatialBlend;
    }

    // Update is called once per frame
    void Update()
    {
        //loudness = GetAverageVolume() * sensitivity;
        //loudness += loudness;
    }

    /*float GetAverageVolume()
    {
        float[] data = new float[256];
        float a = 0;
        audioSGen.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a / 256;
    }*/

    //public void JigglePrefab()
    //{
            //prefab.transform.localScale += new Vector3(loudness, loudness, loudness);
    //}


    IEnumerator GenerateAudiObject(string filepath, string filename, AudioClip GenClip)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            
            filepath = Path.Combine("file://" + Application.persistentDataPath, filename + ".wav");
            Debug.Log (filepath);
            //pathtext.text = (File.Exists(filepath) ? "Android - File exists at" + filepath : "File does not exist at" + filepath);
        }
        else
        {
            filepath = Path.Combine(Application.persistentDataPath, filename + ".wav");
            //pathtext.text = (File.Exists(filepath) ? "PC - File exists at" + filepath : "File does not exist at" + filepath);
        }

        photonView.RPC("RPC_SetExistingAudioClip", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.ActorNumber, filepath);
        yield return null;
    }

    public void StartRecording()
    {
        if (!generated)
        {
            PersonalManager.instance.mainRecorder.TransmitEnabled = false;
            PersonalManager.instance.mainRecorder.IsRecording = false;
            photonView.RPC("RPC_SetColor", RpcTarget.All, duringRecordingColor.r, duringRecordingColor.g, duringRecordingColor.b, duringRecordingColor.a);
            recording = true;
            audioSource.clip = Microphone.Start(Microphone.devices[0], true, loopDuration, 22050);  // third argument restrict the duration of the audio to 10 seconds 
            while (!(Microphone.GetPosition(null) > 0)) { }
            samplesData = new float[audioSource.clip.samples * audioSource.clip.channels];
            audioSource.clip.GetData(samplesData, 0);
        }
    }

    [PunRPC]
    public void RPC_SetColor(float r, float g, float b, float a)
    {
        Color color = new Color(r, g, b, a);
        if (meshRenderer)
        {
            meshRenderer.material.color = color;
        }
        else if (skinnedMeshRenderer)
        {
            skinnedMeshRenderer.material.color = color;
        }
    }

    public void StopRecording()
    {
        if (recording)
        {
            photonView.RPC("RPC_SetColor", RpcTarget.All, postRecordingColor.r, postRecordingColor.g, postRecordingColor.b, postRecordingColor.a);
            Debug.Log(filename);

            // Delete the file if it exists.
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
            try
            {
                //Microphone.End(_SelectedDevice);
                recording = false;
                if (!recording && !generated)
                {
                    filename = (gameObject.name + "-" + photonView.ViewID);
                    //filename = ("clip" + DateTime.Now.ToString("yyyymmdd--HH-mm-ss"));
                    filepath = Path.Combine(Application.persistentDataPath, filename + ".wav");
                    SavWav.Save(filename, audioSource.clip);
                    Debug.Log("File Saved Successfully at: " + filepath);
                }

            }
            catch (DirectoryNotFoundException)
            {
                Debug.LogError("this is not the folder you need. It will appear in the application.persistentDatapath folder. ");
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);    //check for other Exceptions 
            }

            if (!generated && !recording)
            {
                StartCoroutine(GenerateAudiObject(filepath, filename, audioSource.clip));
                photonView.RPC("RPC_SetGenerated", RpcTarget.AllBuffered, true);
                audioCreator = PhotonNetwork.LocalPlayer.ActorNumber;
            }

            recorder.IsRecording = true;
            recorder.TransmitEnabled = true;
        }
    }

    [PunRPC]
    public void RPC_SetExistingAudioClip(int actorNum, string _filePath)
    {
        //Only set the audioclip if we created/own it (we have it local disk)
        if(PhotonNetwork.LocalPlayer.ActorNumber == actorNum)
        {
            StartCoroutine(SetExistingAudioClip(actorNum, _filePath));
        }
        //We still store this data in everyone's instances
        audioCreator = actorNum;
        filepath = _filePath;
    }

    IEnumerator SetExistingAudioClip(int actorNum, string _filePath)
    {
        UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(_filePath, AudioType.WAV);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            //Set the created audioclip to the recorder component, which will handle transmiting audio over the network
            recorder.AudioClip = DownloadHandlerAudioClip.GetContent(www);
            recorder.AudioClip.name = filename;
            recorder.StartRecording();
            recorder.LoopAudioClip = true;
            photonView.RPC("RPC_SetGenerated", RpcTarget.AllBuffered, true);
        }
    }

    //The generated boolean is set over an RPC, so once audio is recorded, other users can't override it
    [PunRPC]
    public void RPC_SetGenerated(bool b)
    {
        generated = b;
    }
}