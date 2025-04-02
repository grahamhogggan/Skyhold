using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    [SerializeField] private Material stars;
    public GameObject usualSun;
    public GameObject newSun;
    // Start is called before the first frame update
    void Start()
    {
        if(Overlord.type == Overlord.WorldType.Space)
        {
            RenderSettings.skybox = stars;
            usualSun.SetActive(false);
            newSun.SetActive(true);
        }
    }

}
