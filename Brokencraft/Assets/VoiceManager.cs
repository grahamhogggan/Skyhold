using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceManager : MonoBehaviour
{
   public void Speak()
   {
    foreach(Voice voice in GetComponentsInChildren<Voice>())
    {
        voice.Reset();
    }
   }
}
