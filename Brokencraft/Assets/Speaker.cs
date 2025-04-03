using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaker : MonoBehaviour
{
    public void Say(string words)
    {
        GetComponent<TextWriter>().SetTarget(words);
        GetComponent<VoiceManager>().Speak();
    }
}
