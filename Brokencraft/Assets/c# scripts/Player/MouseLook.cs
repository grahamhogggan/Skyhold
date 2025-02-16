using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;

    public float minimumVart = -90;
    public float maximumVert = 90;

    private float _rotationX;
    public static bool active = true; 
    // Start is called before the first frame update
    void Start()
    {
        //sensitivityHor = PlayerPrefs.GetFloat("sensitivity");
       // sensitivityVert = PlayerPrefs.GetFloat("sensitivity");
        minimumVart = -90;
        maximumVert = 90;
}

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            Cursor.lockState = CursorLockMode.Locked;
            if (axes == RotationAxes.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
            }
            else if (axes == RotationAxes.MouseY)
            {
                _rotationX -= Input.GetAxis("Mouse Y") * (sensitivityVert);
                _rotationX = Mathf.Clamp(_rotationX, minimumVart, maximumVert);

                float rotationY = transform.localEulerAngles.y;
                transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
            }
            else
            {
                _rotationX -= Input.GetAxis("Mouse Y") * (sensitivityVert);
                _rotationX = Mathf.Clamp(_rotationX, minimumVart, maximumVert);
                float delta = Input.GetAxis("Mouse X") * sensitivityHor;
                float rotationY = transform.localEulerAngles.y + delta;
                transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;

        }
    }
}
