using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitEffect : MonoBehaviour
{
    // Start is called before the first frame update
    Image img;
    void Start()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        img.color = new Color(img.color.r,img.color.g,img.color.b,img.color.a-Time.deltaTime);
    }
    public void Hit()
    {
        img.color = new Color(img.color.r,img.color.g,img.color.b,0.2f);
    }
}
