using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voice : MonoBehaviour
{
    public bool speaking = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.z > 0.01f)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z - Time.deltaTime * 0.5f);

        }
        if (speaking)
        {
            if (transform.localScale.z <= 0.01f)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y,Random.Range(0.1f,0.7f));

            }
        }
    }
}
