using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CutsceneLoader : MonoBehaviour
{ 
    static bool playSplashes = true;
       void Start()
    {
        if(playSplashes)
        {
            SceneManager.LoadScene(15);
            playSplashes = false;
        }
        else if(PersistentData.SceneToPlay ==1 )
        {
            PersistentData.SceneToPlay =0;
            SceneManager.LoadScene(4);
        }
        else if(PersistentData.SceneToPlay!=0)
        {
            int p = PersistentData.SceneToPlay;
            PersistentData.SceneToPlay =0;
            SceneManager.LoadScene(p);
        }
        else
        {
            SceneManager.LoadScene(3);
        }
    }
}
