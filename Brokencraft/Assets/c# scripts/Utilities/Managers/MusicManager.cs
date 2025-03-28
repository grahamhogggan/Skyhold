using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public MusicProfile[] profiles;
    private MusicProfile gameProfile;
    private AudioSource src;
    private int currentClipNumber;
    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
        gameProfile = profiles[(int)Overlord.type];
        src.clip = gameProfile.tracks[0];
        src.Play();
        Debug.Log("set clip to: "+src.clip.name);
        currentClipNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(src.isPlaying == false)
        {
            int clipNum = Random.Range(0,gameProfile.tracks.Count);
            if(clipNum == currentClipNumber)
            {
                if(currentClipNumber == 0)
                {
                    clipNum = 1;
                }
                else clipNum = 0;
            }
            currentClipNumber = clipNum;
            src.clip = gameProfile.tracks[clipNum];
            src.Play();
        }
    }
}
[System.Serializable]
public struct MusicProfile
{
    public List<AudioClip> tracks;
}