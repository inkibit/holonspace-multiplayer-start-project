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
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class MakeAudioLoopObject : MonoBehaviourPun
{
    //public float loudness;
    //public float sensitivity;
    public int loopDuration;
    //public float minScale;
    private string _SelectedDevice;
    private string[] devices;
    static float[] samplesData;
    public bool recording;
    public bool generated;
    private string filename;
    private string filepath;
    private int audioCreator;
    private Recorder photonRecorder;
    //private string packageName;
    //public Text pathtext;
    //public float x, y, z;

    // Start is called before the first frame update

    private void Awake()
    {
        recording = false;
        generated = false;
    }

    void Start()
    {
        //sensitivity = 100;
        loopDuration = 4;
        //minScale = 0.5f;
        _SelectedDevice = Microphone.devices[0].ToString();
        //packageName = "com." + Application.companyName + "." + Application.productName;
        //Debug.Log(packageName);
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
        AudioSource audioS = gameObject.GetComponent<AudioSource>();

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
            AudioSource audioS = gameObject.GetComponent<AudioSource>();
            GetComponentInChildren<Renderer>().material.color = Color.blue;
            recording = true;
            audioS.clip = Microphone.Start(Microphone.devices[0], true, loopDuration, 22050);  // third argument restrict the duration of the audio to 10 seconds 
            while (!(Microphone.GetPosition(null) > 0)) { }
            samplesData = new float[audioS.clip.samples * audioS.clip.channels];
            audioS.clip.GetData(samplesData, 0);
        }
    }

    public void StopRecording()
    {
        if (recording)
        {
            GetComponentInChildren<Renderer>().material.color = Color.white;
            Debug.Log(filename);
            AudioSource audioS = gameObject.GetComponent<AudioSource>();

            // Delete the file if it exists.
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
            try
            {
                Microphone.End(_SelectedDevice);
                recording = false;
                if (!recording && !generated)
                {
                    filename = (gameObject.name + "-" + GetComponent<PhotonView>().ViewID);
                    //filename = ("clip" + DateTime.Now.ToString("yyyymmdd--HH-mm-ss"));
                    filepath = Path.Combine(Application.persistentDataPath, filename + ".wav");
                    SavWav.Save(filename, audioS.clip);
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
                StartCoroutine(GenerateAudiObject(filepath, filename, audioS.clip));
                photonView.RPC("RPC_SetGenerated", RpcTarget.All, true);
                audioCreator = PhotonNetwork.LocalPlayer.ActorNumber;
            }
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
            photonRecorder = GetComponent<Recorder>();
            photonRecorder.AudioClip = DownloadHandlerAudioClip.GetContent(www);
            photonRecorder.AudioClip.name = filename;
            photonRecorder.StartRecording();
            photonRecorder.LoopAudioClip = true;
            photonView.RPC("RPC_SetGenerated", RpcTarget.All, true);
        }
    }

    //The generated boolean is set over an RPC, so once audio is recorded, other users can't override it
    [PunRPC]
    public void RPC_SetGenerated(bool b)
    {
        generated = b;
    }
}