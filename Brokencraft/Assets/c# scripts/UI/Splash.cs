using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    [SerializeField] Image img;
    // Start is called before the first frame update
    void Start()
    {
        img.material.color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    float delta = 0;
    float scl = 0.25f;
    void Update()
    {
        delta += Time.deltaTime * scl;
        img.material.color = new Color(1, 1, 1, delta);
        if (delta > 1)
        {
            scl = -scl;
        }
        if (delta < 0)
        {
            img.material.color = new Color(1, 1, 1, 1);
            img.enabled = false;
            SceneManager.LoadScene(0);

        }
    }
}
