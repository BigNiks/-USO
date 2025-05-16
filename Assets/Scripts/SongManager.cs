using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.IO;
using UnityEngine.Networking;
using System;
using UnityEngine.SceneManagement;

public class SongManager : MonoBehaviour
{
    public static SongManager instance;
    public AudioSource audioSource;
    public Lane[] lanes;
    public float songDelaySeconds;
    public double marginOfErrorSeconds;
    public int inputDelayMiliseconds;
    public EndResultsUI endResultsUI;
    public SongSelectManager songSelectPanel;
    

    public String fileLocation;
    public float noteTime;
    public float noteSpawnY;
    public float noteTapY;
    private bool hasEnded = false;
    public float noteDespawnPosition
    {
        get
        {
            return noteTapY - (noteSpawnY - noteTapY);
        }
    }

    public static MidiFile midiFile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fileLocation = songSelectPanel.midiFile;
        instance = this;
        if (Application.streamingAssetsPath.StartsWith("http://") ||
            Application.streamingAssetsPath.StartsWith("https://"))
        {
            StartCoroutine(ReadFromWebsite());
        }
        else
        {
            ReadFromFile();
        }
        endResultsUI = endResultsUI.GetComponent<EndResultsUI>();
        endResultsUI.endResultsUI.SetActive(false);
    }

    private IEnumerator ReadFromWebsite()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(Application.streamingAssetsPath + "/" + fileLocation))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                byte[] results = www.downloadHandler.data;
                using (var stream = new MemoryStream(results))
                {
                    midiFile = MidiFile.Read(stream);
                    GetDataFromMidi();
                }
            }
        }
    }

    private void ReadFromFile()
    {
        midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + fileLocation);
        GetDataFromMidi();
    }

    public void GetDataFromMidi()
    {
        var notes = midiFile.GetNotes();
        var array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(array, 0);

        foreach (var lane in lanes)
        {
            lane.SetTimeStamps(array);
        }
        
        Invoke(nameof(StartSong), songDelaySeconds);
    }

    public void StartSong()
    {
        audioSource.Play();
    }

    public static double GetAudioSourceTime()
    {
        return (double)instance.audioSource.timeSamples / instance.audioSource.clip.frequency;
    }
    
    // Update is called once per frame
    void Update()
    {
        //added panic key incase player thinks song is too hard
        //*cough bad apple *cough
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        if (!audioSource.isPlaying && !hasEnded)
        {
            hasEnded = true;
            // FindObjectOfType<EndResultsUI>().ShowResults();
            endResultsUI.ShowResults();
        }
    }
}
