using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SongSelectManager : MonoBehaviour
{
    public GameObject songSelectPanel;
    public Button[] songButtons;
    public AudioSource audioSource;
    public GameObject songManager;
    public GameObject endResultsUI;

    public SongInfo[] songs;
    public String midiFile;
    
    [System.Serializable]
    public class SongInfo
    {
        public string songName;
        public AudioClip audioClip;
    }

    void Start()
    {
        songManager.SetActive(false);
        endResultsUI.SetActive(false);
        for (int i = 0; i < songButtons.Length; i++)
        {
            int index = i; //avoid closure bug
            songButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = songs[i].songName;
            songButtons[i].onClick.AddListener(() => SelectSong(index));
        }
    }

    void SelectSong(int songIndex)
    {
        midiFile = songs[songIndex].songName;
        audioSource.clip = songs[songIndex].audioClip;
        songSelectPanel.SetActive(false);
        songManager.SetActive(true); //Start gameplay
    }
}