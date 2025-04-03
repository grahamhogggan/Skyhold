using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceManager : MonoBehaviour
{
   void Update()
   {
    TextWriter writer = GetComponent<TextWriter>();
    if(writer.IsFinishedWriting())
    {
        foreach(Voice voice in GetComponentsInChildren<Voice>())
        {
            voice.speaking = false;
        }
    }
    else
    {
        foreach(Voice voice in GetComponentsInChildren<Voice>())
        {
            voice.speaking = true;
        }
    }
   }
}
