using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CutsceneLoader : MonoBehaviour
{    void Start()
    {
        if(PersistentData.playedScenes ==0 )
        {
            PersistentData.playedScenes =1;
            SceneManager.LoadScene(3);
        }
    }
}
