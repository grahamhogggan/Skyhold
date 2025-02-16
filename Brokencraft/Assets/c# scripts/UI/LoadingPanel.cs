using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingPanel : MonoBehaviour
{
    [SerializeField] private GameObject[] rivalCanvases;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject canvas in rivalCanvases)
        {
            canvas.SetActive(false);
        }
        Overlord.Pause();
    }

    // Update is called once per frame
    public void Begin()
    {
        foreach (GameObject canvas in rivalCanvases)
        {
            canvas.SetActive(true);
        }
        Overlord.Resume();
        gameObject.SetActive(false);
    }
}
