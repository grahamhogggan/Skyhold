using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CutsceneLoader : MonoBehaviour
{    void Start()
    {
        if(PersistentData.SceneToPlay ==1 )
        {
            PersistentData.SceneToPlay =0;
            SceneManager.LoadScene(4);
        }
        else
        {
            SceneManager.LoadScene(3);
        }
    }
}
