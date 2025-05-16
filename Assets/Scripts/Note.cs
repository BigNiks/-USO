using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    double time;

    public float assignedTime; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = SongManager.GetAudioSourceTime();
    }

    // Update is called once per frame
    void Update()
    {
        double timeSinceInstantiated = SongManager.GetAudioSourceTime() - time;
        float t = (float)(timeSinceInstantiated / (SongManager.instance.noteTime * 2));
        
        if (t > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(Vector3.up * SongManager.instance.noteSpawnY, Vector3.up * SongManager.instance.noteDespawnPosition, t); 
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
