using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRegisteration : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Chunk.mainCamera = gameObject;
    }

}
